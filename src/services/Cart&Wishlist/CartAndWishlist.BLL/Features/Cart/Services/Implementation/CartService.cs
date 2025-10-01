using System.Data.Common;
using AutoMapper;
using CartAndWishlist.BLL.Features.Cart.DTOs.Requests;
using CartAndWishlist.BLL.Features.Cart.DTOs.Responses;
using CartAndWishlist.BLL.Features.Cart.Services.Interfaces;
using CartAndWishlist.DAL.Repositories.UOW.Interfaces;
using CartAndWishlist.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.ErrorHandling;
using Shared.Exceptions;

namespace CartAndWishlist.BLL.Features.Cart.Services.Implementation;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCartRequest> _createCartRequestValidator;
    private readonly IValidator<GetCartByMemberIdRequest> _getCartByMemberIdValidator;
    private readonly ILogger<CartService> _logger;
    
    public CartService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<GetCartByMemberIdRequest> getCartByMemberIdValidator, IValidator<CreateCartRequest> createCartRequestValidator, ILogger<CartService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _getCartByMemberIdValidator = getCartByMemberIdValidator;
        _createCartRequestValidator = createCartRequestValidator;
        _logger = logger;
    }
    
    public async Task<Result<CartDto?>> CreateCartAsync(CreateCartRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _createCartRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<CartDto?>.NotFound(key: request.MemberId, entityName: nameof(Domain.Models.Cart));
        }

        var cart = _mapper.Map<Domain.Models.Cart>(request);
        cart.CartId = Guid.CreateVersion7();
        cart.CreatedAt = DateTimeOffset.UtcNow;
        cart.Status = CartStatus.Active;

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            
            await _unitOfWork.CartRepository.CreateCartAsync(cart, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();
            return Result<CartDto?>.Created($"/api/carts/{cart.CartId}", _mapper.Map<CartDto>(cart));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operations");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<CartDto?>> GetCartByIdAsync(GetCartByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var cart = await _unitOfWork.CartRepository.GetCartAsync(request.CartId, cancellationToken);

            if (cart is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<CartDto?>.NotFound(key: request.CartId, entityName: nameof(Domain.Models.Cart));
            }

            var cartDto = _mapper.Map<CartDto>(cart);

            await _unitOfWork.CommitTransactionAsync();
            return Result<CartDto?>.Ok(cartDto);
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }

    public async Task<Result<CartDto?>> GetCartByMemberIdAsync(GetCartByMemberIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _getCartByMemberIdValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<CartDto?>.NotFound(key: request.MemberId, entityName: "Member");
            }
            
            await _unitOfWork.BeginTransactionAsync();
            var cart = await _unitOfWork.CartRepository.GetCartByMemberIdAsync(request.MemberId, cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
            return cart is null 
                ? Result<CartDto?>.NotFound(alternativeMessage: $"Cart that belongs to the member with ID of {request.MemberId} was not found") 
                : Result<CartDto?>.Ok(_mapper.Map<CartDto?>(cart));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<CartDto?>> UpdateCartAsync(UpdateCartRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var cart = await _unitOfWork.CartRepository.GetCartAsync(request.CartId, cancellationToken);
            if (cart is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<CartDto?>.NotFound(key: request.CartId, nameof(Domain.Models.Cart));
            }

            cart.Status = request.Status;

            await _unitOfWork.CartRepository.UpdateCartAsync(request.CartId, cart, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();
            return Result<CartDto?>.Ok(_mapper.Map<CartDto>(cart));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<bool>> DeleteCartAsync(DeleteCartRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var success = await _unitOfWork.CartRepository.DeleteCartAsync(request.CartId, cancellationToken);
            
            await _unitOfWork.CommitTransactionAsync();

            return success
                ? Result<bool>.NoContent()
                : Result<bool>.NotFound(key: request.CartId, entityName: nameof(Domain.Models.Cart));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
}