using CartAndWishlist.Domain.Models;

namespace CartAndWishlist.DAL.Repositories.Interfaces;

public interface IWishlistRepository
{
    Task<Wishlist?> GetWishlistAsync(Guid wishlistId, CancellationToken cancellationToken);
    Task<Wishlist> CreateWishlistAsync(Wishlist wishlist, CancellationToken cancellationToken);
    Task<Wishlist> UpdateWishlistAsync(Guid wishlistId, Wishlist wishlist, CancellationToken cancellationToken);
    Task<bool> DeleteWishlistAsync(Guid wishlistId, CancellationToken cancellationToken);
    Task<Wishlist?> GetWishlistByMemberId(Guid memberId, CancellationToken cancellationToken);
}