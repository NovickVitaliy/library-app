namespace CartAndWishlist.Domain.Models;

public class Wishlist
{
    public Guid WishlistId { get; set; }
    public Guid MemberId { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public List<WishlistItem> WishlistItems { get; set; } = [];
}