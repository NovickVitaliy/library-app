using System.Data;
using System.Data.Common;
using CartAndWishlist.DAL.Repositories.Interfaces;
using CartAndWishlist.Domain.Models;
using Dapper;

namespace CartAndWishlist.DAL.Repositories.Implementations;

public class CartRepository : RepositoryBase, ICartRepository
{
    public CartRepository(DbConnection? connection = null, DbTransaction? transaction = null)
    {
        Connection = connection;
        Transaction = transaction;
    }

    public async Task<Cart?> GetCartAsync(Guid cartId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        var cartDictionary = new Dictionary<Guid, Cart>();

        var cmd = new CommandDefinition(
            """
            SELECT carts.cart_id, carts.member_id, carts.created_at, carts.status, 
                                cart_items.cart_item_id, cart_items.cart_id, cart_items.book_id, cart_items.quantity, cart_items.added_at FROM carts
                              LEFT JOIN cart_items 
                              ON carts.cart_id = cart_items.cart_id 
                              WHERE carts.cart_id = @CartId
            """,
            new { CartId = cartId },
            transaction: Transaction,
            cancellationToken: cancellationToken);

        await Connection.QueryAsync<Cart, CartItem, Cart>(
            cmd,
            (cart, cartItem) =>
            {
                if (!cartDictionary.TryGetValue(cart.CartId, out var currentCart))
                {
                    currentCart = cart;
                    cart.CartItems = [];
                    cartDictionary.Add(cart.CartId, currentCart);
                }

                if (cartItem is not null)
                {
                    currentCart.CartItems.Add(cartItem);
                }

                return currentCart;
            },
            splitOn: "cart_item_id");

        return cartDictionary.Values.FirstOrDefault();
    }

    public async Task<Cart> CreateCartAsync(Cart cart, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        var cmd = new CommandDefinition(
            commandText: "create_cart",
            new
            {
                p_cart_id = cart.CartId,
                p_member_id = cart.MemberId,
                p_created_at = cart.CreatedAt,
                p_status = cart.Status.ToString()
            },
            commandType: CommandType.StoredProcedure,
            transaction: Transaction,
            cancellationToken: cancellationToken
        );
        await Connection.ExecuteAsync(cmd);

        return cart;
    }

    public async Task<Cart> UpdateCartAsync(Guid cartId, Cart cart, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "UPDATE carts SET status = @Status WHERE cart_id = @CartId",
            new { Status = cart.Status.ToString(), CartId = cartId },
            transaction: Transaction,
            cancellationToken: cancellationToken);

        await Connection.ExecuteAsync(cmd);

        return cart;
    }

    public async Task<bool> DeleteCartAsync(Guid cartId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "DELETE FROM carts WHERE cart_id = @CartId",
            new { CartId = cartId },
            transaction: Transaction,
            cancellationToken: cancellationToken
        );
        var rowsDeleted = await Connection.ExecuteAsync(cmd);
        return rowsDeleted == 1;
    }

    public async Task<List<Cart>> GetCartsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var skip = (pageNumber - 1) * pageSize;
        var cmd = new CommandDefinition(
            "SELECT * FROM carts OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY",
            new { Skip = skip, Take = pageSize },
            transaction: Transaction,
            cancellationToken: cancellationToken);

        return (await Connection.QueryAsync<Cart>(cmd)).ToList();
    }

    public async Task<Cart?> GetCartByMemberIdAsync(Guid memberId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        var cmd = new CommandDefinition(
            "SELECT * FROM carts WHERE member_id = @MemberId",
            new { MemberId = memberId },
            transaction: Transaction,
            cancellationToken: cancellationToken);

        return await Connection.QuerySingleOrDefaultAsync<Cart>(cmd);
    }
}