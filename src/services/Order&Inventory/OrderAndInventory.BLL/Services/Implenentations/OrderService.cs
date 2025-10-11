using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using OrderAndInventory.BLL.DTOs.Order.Requests;
using OrderAndInventory.BLL.DTOs.Order.Responses;
using OrderAndInventory.BLL.DTOs.OrderItem.Requests;
using OrderAndInventory.BLL.Services.Interfaces;
using OrderAndInventory.BLL.Specifications;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.UOW.Interfaces;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Implenentations;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    private readonly IValidator<CreateOrderItemRequest> _createOrderItemRequestValidator;
    
    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger, IValidator<CreateOrderItemRequest> createOrderItemRequestValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _createOrderItemRequestValidator = createOrderItemRequestValidator;
    }
    
    public async Task<Result<OrderDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching order with ID: {OrderId}", id);

        var order = await _unitOfWork.OrderRepository.GetOrderWithRelatedEntities(id, true, true, true, true, cancellationToken);
        if (order is null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found", id);
            return Result<OrderDto?>.NotFound(key: id, nameof(Order));
        }
        
        _logger.LogInformation("Order with ID {OrderId} retrieved successfully", id);
        return Result<OrderDto?>.Ok(_mapper.Map<OrderDto>(order));
    }
    
    public async Task<Result<PaginationResult<OrderDto>>> GetAsync(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching orders with filters: {@Request}", request);
        var specification = new OrderSpecification(request);
        var orders = (await _unitOfWork.OrderRepository.ListBySpecAsync(specification, cancellationToken)).ToList();
        var totalCount = await _unitOfWork.OrderRepository.CountBySpecAsync(new OrderSpecification(request, true), cancellationToken);

        return Result<PaginationResult<OrderDto>>.Ok(new PaginationResult<OrderDto>(
                orders.Select(_mapper.Map<OrderDto>).ToArray(),
                totalCount,
                request.PageNumber,
                Math.Ceiling((decimal)totalCount / request.PageSize),
                request.PageSize));
    }
    
    public async Task<Result<OrderDto?>> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await Task.WhenAll(request.OrderItems.Select(x => _createOrderItemRequestValidator.ValidateAsync(x, cancellationToken)));
        if (validationResult.Any(x => !x.IsValid))
        {
            return Result<OrderDto?>.BadRequest(validationResult.First(x => !x.IsValid).Errors[0].ErrorMessage);
        }

        var member = await _unitOfWork.MemberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        if (member is null)
        {
            return Result<OrderDto?>.NotFound(key: request.MemberId, nameof(Member));
        }
        
        var order = _mapper.Map<Order>(request);
        order.OrderId = Guid.CreateVersion7();
        order.Status = OrderStatus.Pending;
        foreach (var orderItem in order.OrderItems)
        {
            orderItem.OrderId = order.OrderId;
        }
        
        foreach (var staff in order.StaffOrders)
        {
            staff.OrderId = order.OrderId;
        }

        await _unitOfWork.OrderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<OrderDto?>.Created($"/api/orders/{order.OrderId}", _mapper.Map<OrderDto>(order));
    }
    
    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
        if (order is null)
        {
            return Result<bool>.NotFound(key: id, entityName: nameof(Order));
        }

        await _unitOfWork.OrderRepository.RemoveAsync(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.NoContent();
    }
}