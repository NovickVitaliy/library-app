using CartAndWishlist.Domain.Models;

namespace CartAndWishlist.DAL.Repositories.Interfaces;

public interface ICartItemRepository
{
    Task<CartItem?> GetCartItemAsync(Guid cartItemId, CancellationToken cancellationToken);
    Task<CartItem> CreateCartItemAsync(CartItem cartItem, CancellationToken cancellationToken);
    Task<CartItem> UpdateCartItemAsync(Guid cartItemId, CartItem cartItem, CancellationToken cancellationToken);
    Task<bool> DeleteCartItemAsync(Guid cartItemId, CancellationToken cancellationToken);
    Task<List<CartItem>> GetCartItemsAsync(Guid cartId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAllAsync(Guid cartId, CancellationToken cancellationToken);
}