using System.Data.Common;
using AutoMapper;
using CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;
using CartAndWishlist.BLL.Features.Wishlist.DTOs.Responses;
using CartAndWishlist.BLL.Features.Wishlist.Services.Interfaces;
using CartAndWishlist.DAL.Repositories.UOW.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.ErrorHandling;
using Shared.Exceptions;

namespace CartAndWishlist.BLL.Features.Wishlist.Services.Implementations;

public class WishlistService : IWishlistService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WishlistService> _logger;
    private readonly IValidator<CreateWishlistRequest> _createWishlistRequstValidator;
    private readonly IValidator<GetWishlistByMemberIdRequest> _getWishlistByMemberIdRequestValidator;
    private readonly IMapper _mapper;
    
    public WishlistService(IUnitOfWork unitOfWork, ILogger<WishlistService> logger, IValidator<CreateWishlistRequest> createWishlistRequstValidator, IValidator<GetWishlistByMemberIdRequest> getWishlistByMemberIdRequestValidator, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _createWishlistRequstValidator = createWishlistRequstValidator;
        _getWishlistByMemberIdRequestValidator = getWishlistByMemberIdRequestValidator;
        _mapper = mapper;
    }
    public async Task<Result<WishlistDto>> GetWishlistByIdAsync(GetWishlistRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistAsync(request.WishlistId, cancellationToken);
            if (wishlist is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<WishlistDto>.NotFound(key: request.WishlistId, entityName: nameof(Domain.Models.Wishlist));
            }
            
            await _unitOfWork.CommitTransactionAsync();
            return Result<WishlistDto>.Ok(_mapper.Map<WishlistDto>(wishlist));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    public async Task<Result<WishlistDto>> CreateWishlistAsync(CreateWishlistRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _createWishlistRequstValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<WishlistDto>.NotFound(alternativeMessage: $"Member with ID of {request.MemberId} was not found");
        }
        
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var wishlist = _mapper.Map<Domain.Models.Wishlist>(request);
            wishlist.WishlistId = Guid.CreateVersion7();
            wishlist.CreatedAt = DateTimeOffset.UtcNow;

            await _unitOfWork.WishlistRepository.CreateWishlistAsync(wishlist, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();
            
            return Result<WishlistDto>.Created($"/api/wishlists/{wishlist.WishlistId}", _mapper.Map<WishlistDto>(wishlist));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    public async Task<Result<WishlistDto>> UpdateWishlistAsync(Guid wishlistId, UpdateWishlistRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistAsync(wishlistId, cancellationToken);
            if (wishlist is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<WishlistDto>.NotFound(key: wishlistId, entityName: nameof(Domain.Models.Wishlist));
            }
            
            wishlist.Name = request.Name;

            await _unitOfWork.WishlistRepository.UpdateWishlistAsync(wishlistId, wishlist, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();
            
            return Result<WishlistDto>.Ok(_mapper.Map<WishlistDto>(wishlist));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    public async Task<Result<bool>> DeleteWishlistAsync(DeleteWishlistRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistAsync(request.WishlistId, cancellationToken);
            if (wishlist is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<bool>.NotFound(key: request.WishlistId, entityName: nameof(Domain.Models.Wishlist));
            }

            await _unitOfWork.WishlistRepository.DeleteWishlistAsync(request.WishlistId, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();
            
            return Result<bool>.NoContent();
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<WishlistDto>> GetWishlistByMemberId(GetWishlistByMemberIdRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _getWishlistByMemberIdRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<WishlistDto>.NotFound(alternativeMessage: $"Member with ID of {request.MemberId} was not found");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByMemberId(request.MemberId, cancellationToken);
            if (wishlist is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<WishlistDto>.NotFound(alternativeMessage: $"Wishlist that belongs to the member with ID of '{request.MemberId}' was not found");
            }
            
            await _unitOfWork.CommitTransactionAsync();
            
            return Result<WishlistDto>.Ok(_mapper.Map<WishlistDto>(wishlist));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
}