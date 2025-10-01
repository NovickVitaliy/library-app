using System.Data;
using System.Data.Common;
using CartAndWishlist.DAL.Database.ConnectionAccessor;
using CartAndWishlist.DAL.Repositories.Interfaces;
using CartAndWishlist.DAL.Repositories.UOW.Interfaces;

namespace CartAndWishlist.DAL.Repositories.UOW.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDatabaseConnectionAccessor _databaseConnectionAccessor;
    private DbConnection? _connection;
    private DbTransaction? _transaction;
    
    public ICartItemRepository CartItemRepository { get; }
    public ICartRepository CartRepository { get; }
    public IWishlistRepository WishlistRepository { get; }
    public IWishlistItemRepository WishlistItemRepository { get; }

    public UnitOfWork(
        ICartItemRepository cartItemRepository, 
        ICartRepository cartRepository, 
        IWishlistRepository wishlistRepository, 
        IWishlistItemRepository wishlistItemRepository, 
        IDatabaseConnectionAccessor databaseConnectionAccessor)
    {
        CartItemRepository = cartItemRepository;
        CartRepository = cartRepository;
        WishlistRepository = wishlistRepository;
        WishlistItemRepository = wishlistItemRepository;
        _databaseConnectionAccessor = databaseConnectionAccessor;
    }

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
    {
        if (_connection != null)
            throw new InvalidOperationException("Transaction already started.");
        
        _connection = _databaseConnectionAccessor.GetConnection();
        await _connection.OpenAsync();
        _transaction = await _connection.BeginTransactionAsync(isolationLevel);
        
        (CartItemRepository as RepositoryBase).SetTransaction(_connection, _transaction);
        (CartRepository as RepositoryBase).SetTransaction(_connection, _transaction);
        (WishlistRepository as RepositoryBase).SetTransaction(_connection, _transaction);
        (WishlistItemRepository as RepositoryBase).SetTransaction(_connection, _transaction);
    }
    
    public async Task CommitTransactionAsync()
    {
        if (_transaction == null) throw new InvalidOperationException("Transaction is not started");

        await _transaction.CommitAsync();
        await _connection!.CloseAsync();
        await CleanupAsync();
    }
    
    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null) throw new InvalidOperationException("Tranasction is not started");

        await _transaction.RollbackAsync();
        await CleanupAsync();
    }
    
    public async ValueTask DisposeAsync()
    {
        await CleanupAsync();
    }
    
    private async Task CleanupAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        if (_connection != null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}