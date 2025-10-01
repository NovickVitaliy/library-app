namespace CartAndWishlist.Domain.Models;

public class WishlistItem
{
    public Guid WishlistItemId { get; set; }
    public Guid WishlistId { get; set; }
    public Wishlist Wishlist { get; set; } = null!;
    public Guid BookId { get; set; }
    public DateTimeOffset AddedAt { get; set; }
}