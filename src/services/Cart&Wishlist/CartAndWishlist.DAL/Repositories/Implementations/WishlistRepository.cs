using System.Data;
using System.Data.Common;
using CartAndWishlist.DAL.Repositories.Interfaces;
using CartAndWishlist.Domain.Models;
using Npgsql;

namespace CartAndWishlist.DAL.Repositories.Implementations;

public class WishlistRepository : RepositoryBase, IWishlistRepository
{
    public WishlistRepository(DbConnection? connection = null, DbTransaction? transaction = null)
    {
        Connection = connection;
        Transaction = transaction;
    }

    public async Task<Wishlist?> GetWishlistAsync(Guid wishlistId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        await using var cmd = Connection.CreateCommand();
        cmd.Transaction = Transaction;
        cmd.CommandText = @"SELECT W.wishlist_id, W.member_id, W.name, W.created_at, 
                            WI.wishlist_item_id, WI.wishlist_id AS item_wishlist_id, WI.book_id, WI.added_at FROM wishlists W 
                            LEFT JOIN wishlist_items WI 
                            ON W.wishlist_id = WI.wishlist_id WHERE W.wishlist_id = @WishlistId";
        
        cmd.Parameters.Add(new NpgsqlParameter("WishlistId", wishlistId));

        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        Wishlist? wishlist = null;
        
        while (await reader.ReadAsync(cancellationToken))
        {
            if (wishlist is null)
            {
                var memberId = reader.GetGuid("member_id");
                var name = reader.GetString("name");
                var createdAt = await reader.GetFieldValueAsync<DateTimeOffset>("created_at", cancellationToken: cancellationToken);
                wishlist = new Wishlist()
                {
                    WishlistId = wishlistId,
                    CreatedAt = createdAt,
                    MemberId = memberId,
                    Name = name,
                    WishlistItems = []
                };   
            }

            if (!await reader.IsDBNullAsync("wishlist_item_id", cancellationToken: cancellationToken))
            {
                var wishlistItem = new WishlistItem()
                {
                    BookId = reader.GetGuid("book_id"),
                    AddedAt = await reader.GetFieldValueAsync<DateTimeOffset>("added_at", cancellationToken: cancellationToken),
                    WishlistId = reader.GetGuid("item_wishlist_id"),
                    WishlistItemId = reader.GetGuid("wishlist_item_id")
                };
                
                wishlist.WishlistItems.Add(wishlistItem);
            }
        }

        return wishlist;
    }

    public async Task<Wishlist> CreateWishlistAsync(Wishlist wishlist, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        await using var cmd = Connection.CreateCommand();
        cmd.Transaction = Transaction;
        cmd.CommandText = "CALL add_wishlist(@wishlistId, @memberId, @name, @createdAt)";
        cmd.Parameters.Add(new NpgsqlParameter("@wishlistId", wishlist.WishlistId));
        cmd.Parameters.Add(new NpgsqlParameter("@memberId", wishlist.MemberId));
        cmd.Parameters.Add(new NpgsqlParameter("@name", wishlist.Name));
        cmd.Parameters.Add(new NpgsqlParameter("@createdAt", wishlist.CreatedAt));

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        return wishlist;
    }

    public async Task<Wishlist> UpdateWishlistAsync(Guid wishlistId, Wishlist wishlist, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        await using var cmd = Connection.CreateCommand();
        cmd.Transaction = Transaction;

        cmd.CommandText = "UPDATE wishlists SET name = @name WHERE wishlist_id = @wishlistId";
        cmd.Parameters.Add(new NpgsqlParameter("name", wishlist.Name));
        cmd.Parameters.Add(new NpgsqlParameter("wishlistId", wishlistId));

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        return wishlist;
    }

    public async Task<bool> DeleteWishlistAsync(Guid wishlistId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();

        await using var cmd = Connection.CreateCommand();
        cmd.Transaction = Transaction;
        cmd.CommandText = "DELETE FROM wishlists WHERE wishlist_id = @wishlistId";
        cmd.Parameters.Add(new NpgsqlParameter("wishlistId", wishlistId));

        var rowsDeleted = await cmd.ExecuteNonQueryAsync(cancellationToken);

        return rowsDeleted == 1;
    }
    
    public async Task<Wishlist?> GetWishlistByMemberId(Guid memberId, CancellationToken cancellationToken)
    {
        ThrowIfConnectionOrTransactionIsUninitialized();
        await using var cmd = Connection.CreateCommand();
        cmd.Transaction = Transaction;
        cmd.CommandText = "SELECT * FROM wishlists WHERE member_id = @memberId";
        cmd.Parameters.Add(new NpgsqlParameter("memberId", memberId));

        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var wishlistId = reader.GetGuid("wishlist_id");
            var name = reader.GetString("name");
            var createdAt = await reader.GetFieldValueAsync<DateTimeOffset>("created_at", cancellationToken: cancellationToken);
            return new Wishlist()
            {
                WishlistId = wishlistId,
                CreatedAt = createdAt,
                MemberId = memberId,
                Name = name
            };
        }

        return null;
    }
}