using CartAndWishlist.Domain.Enums;

namespace CartAndWishlist.Domain.Models;

public class Cart
{
    public Guid CartId { get; set; }
    public Guid MemberId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public CartStatus Status { get; set; }
    public List<CartItem> CartItems { get; set; } = [];
}