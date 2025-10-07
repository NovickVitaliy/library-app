using System.Data;
using System.Data.Common;
using CartAndWishlist.DAL.Repositories.Interfaces;
using CartAndWishlist.Domain.Models;
using Dapper;

namespace CartAndWishlist.DAL.Repositories.Implementations;

public class WishlistItemRepository : RepositoryBase, IWishlistItemRepository
{
    public WishlistItemRepository(DbConnection? connection = null, DbTransaction? transaction = null)
    {
        Connection = connection;
        Transaction = transaction;
    }
    
    public async Task<WishlistItem?> GetWishlistItemAsync(Guid wishlistItemId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "SELECT * FROM wishlist_items WHERE wishlist_item_id = @WishlistItemId",
            new {WishlistItemId = wishlistItemId},
            transaction: Transaction,
            cancellationToken: cancellationToken);
        return await Connection.QuerySingleOrDefaultAsync<WishlistItem>(cmd);
    }
    
    public async Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "add_wishlist_item",
            new
            {
                p_wishlist_item_id = wishlistItem.WishlistItemId,
                p_wishlist_id = wishlistItem.WishlistId,
                p_book_id = wishlistItem.BookId,
                p_added_at = wishlistItem.AddedAt
            },
            commandType: CommandType.StoredProcedure,
            transaction: Transaction,
            cancellationToken: cancellationToken);
        
        await Connection.ExecuteAsync(cmd);
        return wishlistItem;
    }
    
    public async Task<bool> DeleteWishlistItemAsync(Guid wihslistItemId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "DELETE FROM wishlist_items WHERE wishlist_item_id = @WishlistItemId",
            new {WishlistItemId = wihslistItemId},
            transaction: Transaction,
            cancellationToken: cancellationToken);
        
        var rowsDeleted = await Connection.ExecuteAsync(cmd);
        return rowsDeleted == 1;
    }
    
    public async Task<List<WishlistItem>> GetWishlistItemsAsync(Guid wishlistId, int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var skip = (pageNumber - 1) * pageSize;

        var cmd = new CommandDefinition(
            "SELECT * FROM wishlist_items WHERE wishlist_id = @WishlistId OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY",
            new {WishlistId = wishlistId, Skip = skip, Take = pageSize},
            transaction: Transaction,
            cancellationToken: cancellationToken);
        
        return (await Connection.QueryAsync<WishlistItem>(cmd)).ToList();
    }
    
    public async Task<long> CountAllWishlistItemsInWishlistAsync(Guid wishlistId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
                "SELECT COUNT(*) FROM wishlist_items WHERE wishlist_id = @WishlistId",
                new {WishlistId = wishlistId},
                transaction: Transaction,
                cancellationToken: cancellationToken);

        return await Connection.ExecuteScalarAsync<long>(cmd);
    }
}