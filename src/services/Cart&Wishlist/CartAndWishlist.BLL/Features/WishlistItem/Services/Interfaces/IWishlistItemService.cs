using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Responses;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace CartAndWishlist.BLL.Features.WishlistItem.Services.Interfaces;

public interface IWishlistItemService
{
    Task<Result<WishlistItemDto>> GetWishlistItemAsync(GetWishlistItemByIdRequest request, CancellationToken cancellationToken);
    Task<Result<WishlistItemDto>> CreateWishlistItemAsync(CreateWishlistItemRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteWishlistItemAsync(DeleteWishlistItemRequest request, CancellationToken cancellationToken);
    Task<Result<PaginationResult<WishlistItemDto>>> GetWishlistItemsAsync(Guid id, GetWishlistItemsRequest request, CancellationToken cancellationToken);
}