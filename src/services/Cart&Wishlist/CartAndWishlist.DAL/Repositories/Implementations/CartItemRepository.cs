using System.Data;
using System.Data.Common;
using CartAndWishlist.DAL.Repositories.Interfaces;
using CartAndWishlist.Domain.Models;
using Dapper;

namespace CartAndWishlist.DAL.Repositories.Implementations;

public class CartItemRepository : RepositoryBase, ICartItemRepository
{
    public CartItemRepository(DbConnection? connection = null, DbTransaction? transaction = null)
    {
        Connection = connection;
        Transaction = transaction;
    }

    public async Task<CartItem?> GetCartItemAsync(Guid cartItemId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        
        var cmd = new CommandDefinition(
            "SELECT * FROM cart_items WHERE cart_item_id = @Id",
            new { Id = cartItemId },
            cancellationToken: cancellationToken,
            transaction: Transaction);
        
        return await Connection.QuerySingleOrDefaultAsync<CartItem?>(cmd);
    }

    public async Task<CartItem> CreateCartItemAsync(CartItem cartItem, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition("create_cart_item",
            new
            {
                p_cart_item_id = cartItem.CartItemId,
                p_cart_id = cartItem.CartId,
                p_book_id = cartItem.BookId,
                p_quantity = cartItem.Quantity,
                p_added_at = cartItem.AddedAt
            },
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken,
            transaction: Transaction);
        
        await Connection.ExecuteAsync(cmd);
        return cartItem;
    }

    public async Task<CartItem> UpdateCartItemAsync(Guid cartItemId, CartItem cartItem, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "UPDATE cart_items SET cart_id = @CartId, book_id = @BookId, quantity = @Quantity WHERE cart_item_id = @CartItemId",
            new { CartId = cartItem.CartId, BookId = cartItem.BookId, CartItemId = cartItemId, Quantity = cartItem.Quantity },
            cancellationToken: cancellationToken,
            transaction: Transaction);
        
        await Connection.ExecuteAsync(cmd);
        return cartItem;
    }

    public async Task<bool> DeleteCartItemAsync(Guid cartItemId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "DELETE FROM cart_items WHERE cart_item_id = @CartItemId",
            new {CartItemId = cartItemId},
            cancellationToken: cancellationToken,
            transaction: Transaction);
        
        var rowsDeleted = await Connection.ExecuteAsync(cmd);
        return rowsDeleted == 1;
    }

    public async Task<List<CartItem>> GetCartItemsAsync(Guid cartId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        
        var skipItems = (pageNumber - 1) * pageSize;
        
        var cmd = new CommandDefinition(
            "SELECT * FROM cart_items WHERE cart_id = @CartId OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY",
            new {CartId = cartId, Skip = skipItems, Take = pageSize},
            cancellationToken: cancellationToken,
            transaction: Transaction);
        return (await Connection.QueryAsync<CartItem>(cmd)).ToList();
    }
    
    public async Task<long> CountAllAsync(Guid cartId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
                "SELECT COUNT(*) FROM cart_items WHERE cart_id = @CartId",
                new {CartId = cartId},
                transaction: Transaction,
                cancellationToken: cancellationToken
            );

        return await Connection.ExecuteScalarAsync<long>(cmd);
    }
}