using CartAndWishlist.Domain.Models;

namespace CartAndWishlist.DAL.Repositories.Interfaces;

public interface IWishlistItemRepository
{
    Task<WishlistItem?> GetWishlistItemAsync(Guid wishlistItemId, CancellationToken cancellationToken);
    Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem, CancellationToken cancellationToken);
    Task<bool> DeleteWishlistItemAsync(Guid wihslistItemId, CancellationToken cancellationToken);
    Task<List<WishlistItem>> GetWishlistItemsAsync(Guid wishlistId, int pageSize, int pageNumber, CancellationToken cancellationToken);
    Task<long> CountAllWishlistItemsInWishlistAsync(Guid wishlistId, CancellationToken cancellationToken);
}