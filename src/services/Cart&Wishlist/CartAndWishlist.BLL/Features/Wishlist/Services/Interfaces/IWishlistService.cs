using CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;
using CartAndWishlist.BLL.Features.Wishlist.DTOs.Responses;
using Shared.ErrorHandling;

namespace CartAndWishlist.BLL.Features.Wishlist.Services.Interfaces;

public interface IWishlistService
{
    Task<Result<WishlistDto>> GetWishlistByIdAsync(GetWishlistRequest request, CancellationToken cancellationToken);
    Task<Result<WishlistDto>> CreateWishlistAsync(CreateWishlistRequest request, CancellationToken cancellationToken);
    Task<Result<WishlistDto>> UpdateWishlistAsync(Guid wishlistId, UpdateWishlistRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteWishlistAsync(DeleteWishlistRequest request, CancellationToken cancellationToken);
    Task<Result<WishlistDto>> GetWishlistByMemberId(GetWishlistByMemberIdRequest request, CancellationToken cancellationToken);
}