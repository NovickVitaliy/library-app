using System.Data;
using CartAndWishlist.DAL.Repositories.Interfaces;

namespace CartAndWishlist.DAL.Repositories.UOW.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    ICartItemRepository CartItemRepository { get; }
    ICartRepository CartRepository { get; }
    IWishlistRepository WishlistRepository { get; }
    IWishlistItemRepository WishlistItemRepository { get; }

    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead);
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}