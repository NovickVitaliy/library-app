namespace CartAndWishlist.Domain.Models;

public class CartItem
{
    public Guid CartItemId { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; } = null!;
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset AddedAt { get; set; }
}