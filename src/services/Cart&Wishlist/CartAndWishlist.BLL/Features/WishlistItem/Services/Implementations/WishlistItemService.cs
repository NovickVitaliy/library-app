using System.Data.Common;
using AutoMapper;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Responses;
using CartAndWishlist.BLL.Features.WishlistItem.Services.Interfaces;
using CartAndWishlist.DAL.Repositories.UOW.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.ErrorHandling;
using Shared.Exceptions;

namespace CartAndWishlist.BLL.Features.WishlistItem.Services.Implementations;

public class WishlistItemService : IWishlistItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WishlistItemService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateWishlistItemRequest> _createWishlistItemRequestValidator;
    
    public WishlistItemService(IUnitOfWork unitOfWork, ILogger<WishlistItemService> logger, IMapper mapper, IValidator<CreateWishlistItemRequest> createWishlistItemRequestValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _createWishlistItemRequestValidator = createWishlistItemRequestValidator;
    }
    
    public async Task<Result<WishlistItemDto>> GetWishlistItemAsync(GetWishlistItemByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            
            var wishlistItem = await _unitOfWork.WishlistItemRepository.GetWishlistItemAsync(request.WishlistItemId, cancellationToken);
            if (wishlistItem is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<WishlistItemDto>.NotFound(key: request.WishlistItemId, entityName: nameof(Domain.Models.WishlistItem));
            }
            
            await _unitOfWork.CommitTransactionAsync();
            
            return Result<WishlistItemDto>.Ok(_mapper.Map<WishlistItemDto>(wishlistItem));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<WishlistItemDto>> CreateWishlistItemAsync(CreateWishlistItemRequest request, CancellationToken cancellationToken)
    {
        var validatioResult = await _createWishlistItemRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validatioResult.IsValid)
        {
            return Result<WishlistItemDto>.NotFound(alternativeMessage: $"Book with ID of {request.BookId} was not found");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistAsync(request.WishlistId, cancellationToken);
            if (wishlist is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<WishlistItemDto>.NotFound(key: request.WishlistId, entityName: nameof(Domain.Models.Wishlist));
            }

            var wishlistItem = _mapper.Map<Domain.Models.WishlistItem>(request);
            wishlistItem.WishlistItemId = Guid.CreateVersion7();
            wishlistItem.AddedAt = DateTimeOffset.UtcNow;

            await _unitOfWork.WishlistItemRepository.CreateWishlistItemAsync(wishlistItem, cancellationToken);
            
            await _unitOfWork.CommitTransactionAsync();
            
            return Result<WishlistItemDto>.Created($"/api/wishlist-items/{wishlistItem.WishlistItemId}", _mapper.Map<WishlistItemDto>(wishlistItem));
        }
        catch (DbException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(e, "Error occured during database operation");
            throw e.ToInfrastructureException();
        }
    }
    
    public async Task<Result<bool>> DeleteWishlistItemAsync(DeleteWishlistItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var wishlistItem = await _unitOfWork.WishlistItemRepository.GetWishlistItemAsync(request.WishlistItemId, cancellationToken);
            if (wishlistItem is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<bool>.NotFound(key: request.WishlistItemId, entityName: nameof(Domain.Models.WishlistItem));
            }

            await _unitOfWork.WishlistItemRepository.DeleteWishlistItemAsync(request.WishlistItemId, cancellationToken);
            
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
    
    public async Task<Result<PaginationResult<WishlistItemDto>>> GetWishlistItemsAsync(Guid id, GetWishlistItemsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistAsync(id, cancellationToken);
            if (wishlist is null)
            {
                await _unitOfWork.CommitTransactionAsync();
                return Result<PaginationResult<WishlistItemDto>>.NotFound(key: id, entityName: nameof(Domain.Models.Wishlist));
            }

            var wishlistItems = await _unitOfWork.WishlistItemRepository.GetWishlistItemsAsync(id, request.PageSize, request.PageNumber, cancellationToken);
            var totalCount = await _unitOfWork.WishlistItemRepository.CountAllWishlistItemsInWishlistAsync(id, cancellationToken);
            
            await _unitOfWork.CommitTransactionAsync();
            
            return Result<PaginationResult<WishlistItemDto>>.Ok(new PaginationResult<WishlistItemDto>(
                    wishlistItems.Select(x => _mapper.Map<WishlistItemDto>(x)).ToArray(),
                    totalCount,
                    request.PageNumber,
                    Math.Ceiling((decimal)(totalCount / request.PageSize)),
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