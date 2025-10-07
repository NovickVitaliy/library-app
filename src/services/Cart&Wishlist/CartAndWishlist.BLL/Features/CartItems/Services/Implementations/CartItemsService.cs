using System.Data.Common;
using AutoMapper;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Responses;
using CartAndWishlist.BLL.Features.CartItems.Services.Interfaces;
using CartAndWishlist.DAL.Repositories.UOW.Interfaces;
using CartAndWishlist.Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.ErrorHandling;
using Shared.Exceptions;

namespace CartAndWishlist.BLL.Features.CartItems.Services.Implementations;

public class CartItemsService : ICartItemsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCartItemRequest> _createCartItemRequestValidator;
    private readonly ILogger<CartItemsService> _logger;
    private readonly IMapper _mapper;
    
    public CartItemsService(IUnitOfWork unitOfWork, IValidator<CreateCartItemRequest> createCartItemRequestValidator, ILogger<CartItemsService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _createCartItemRequestValidator = createCartItemRequestValidator;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Result<CartItemDto>> GetCartItemAsync(GetCartItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            
            var cartItem = await _unitOfWork.CartItemRepository.GetCartItemAsync(request.CartItemId, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();
            return cartItem is null
                ? Result<CartItemDto>.NotFound(key: request.CartItemId, entityName: nameof(CartItem))
                : Result<CartItemDto>.Ok(_mapper.Map<CartItemDto>(cartItem));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<CartItemDto>> CreateCartItemAsync(CreateCartItemRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _createCartItemRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<CartItemDto>.NotFound(alternativeMessage: $"Book with ID of {request.BookId} was not found");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var cart = await _unitOfWork.CartRepository.GetCartAsync(request.CartId, cancellationToken);
            if (cart is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<CartItemDto>.NotFound(key: request.CartId, entityName: nameof(Domain.Models.Cart));
            }

            var cartItem = _mapper.Map<CartItem>(request);
            cartItem.CartItemId = Guid.CreateVersion7();
            cartItem.AddedAt = DateTimeOffset.UtcNow;

            await _unitOfWork.CartItemRepository.CreateCartItemAsync(cartItem, cancellationToken);
            
            await _unitOfWork.CommitTransactionAsync();
            
            return Result<CartItemDto>.Created($"/api/cart-items/{cartItem.CartItemId}", _mapper.Map<CartItemDto>(cartItem));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<CartItemDto>> UpdateCartItemAsync(Guid cartItemId, UpdateCartItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var cartItem = await _unitOfWork.CartItemRepository.GetCartItemAsync(cartItemId, cancellationToken);
            if (cartItem is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<CartItemDto>.NotFound(key: cartItem, entityName: nameof(CartItem));
            }
            
            cartItem.Quantity = request.Quantity;

            await _unitOfWork.CartItemRepository.UpdateCartItemAsync(cartItemId, cartItem, cancellationToken);
            
            await _unitOfWork.CommitTransactionAsync();
            
            return Result<CartItemDto>.Ok(_mapper.Map<CartItemDto>(cartItem));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<bool>> DeleteCartItemAsync(DeleteCartItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            
            var cartItem = await _unitOfWork.CartItemRepository.GetCartItemAsync(request.CartItemId, cancellationToken);
            if (cartItem is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<bool>.NotFound(key: request.CartItemId, entityName: nameof(CartItem));
            }

            var success = await _unitOfWork.CartItemRepository.DeleteCartItemAsync(request.CartItemId, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();

            return success
                ? Result<bool>.NoContent()
                : Result<bool>.NotFound(key: request.CartItemId, entityName: nameof(CartItem));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<PaginationResult<CartItemDto>>> GetCartItemsAsync(Guid id, GetCartItemsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var cartItems = await _unitOfWork.CartItemRepository.GetCartItemsAsync(id, request.PageNumber, request.PageSize, cancellationToken);
            var totalCount = await _unitOfWork.CartItemRepository.CountAllAsync(id, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();

            return Result<PaginationResult<CartItemDto>>.Ok(new PaginationResult<CartItemDto>(
                    cartItems.Select(x => _mapper.Map<CartItemDto>(x)).ToArray(),
                    totalCount,
                    request.PageNumber,
                    Math.Ceiling(totalCount / (decimal)request.PageSize),
                    request.PageSize
                ));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
}