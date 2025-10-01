using CartAndWishlist.Domain.Models;

namespace CartAndWishlist.DAL.Repositories.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartAsync(Guid cartId, CancellationToken cancellationToken);
    Task<Cart> CreateCartAsync(Cart cart, CancellationToken cancellationToken);
    Task<Cart> UpdateCartAsync(Guid cartId, Cart cart, CancellationToken cancellationToken);
    Task<bool> DeleteCartAsync(Guid cartId, CancellationToken cancellationToken);
    Task<List<Cart>> GetCartsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken);
    Task<Cart?> GetCartByMemberIdAsync(Guid memberId, CancellationToken cancellationToken);
}