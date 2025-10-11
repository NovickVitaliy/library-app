using AutoMapper;
using OrderAndInventory.BLL.DTOs.Payment.Requests;
using OrderAndInventory.BLL.DTOs.Payment.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using OrderAndInventory.BLL.Specifications;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.UOW.Interfaces;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Implenentations;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<PaymentDto?>> CreatePaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<PaymentDto?>.NotFound(alternativeMessage: "There is no Order with such id");
        }

        var payment = _mapper.Map<Payment>(request);
        payment.PaymentId = Guid.CreateVersion7();
        payment.PaidDate = DateTimeOffset.UtcNow;

        await _unitOfWork.PaymentRepository.AddAsync(payment, cancellationToken);
        order.Status = OrderStatus.Processing;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<PaymentDto?>.Created($"/api/payments/{payment.PaymentId}", _mapper.Map<PaymentDto>(payment));
    }
    
    public async Task<Result<PaginationResult<PaymentDto>>> GetAsync(GetPaymentsRequest request, CancellationToken cancellationToken)
    {
        var specification = new PaymentSpecification(request);
        var payments = (await _unitOfWork.PaymentRepository.ListBySpecAsync(specification, cancellationToken)).ToList();
        var totalCount = await _unitOfWork.PaymentRepository.CountBySpecAsync(new PaymentSpecification(request, true), cancellationToken);

        return Result<PaginationResult<PaymentDto>>.Ok(new PaginationResult<PaymentDto>(
                payments.Select(_mapper.Map<PaymentDto>).ToArray(),
                totalCount,
                request.PageNumber,
                Math.Ceiling((decimal)totalCount / request.PageSize),
                request.PageSize));
    }
    
    public async Task<Result<PaymentDto?>> GetPaymentAsync(Guid id, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id, cancellationToken);
        return payment is null 
            ? Result<PaymentDto?>.NotFound(key: id, entityName: nameof(Payment)) 
            : Result<PaymentDto?>.Ok(_mapper.Map<PaymentDto>(payment));
    }
}