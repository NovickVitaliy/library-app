namespace OrderAndInventory.DAL.Models;

public class OrderItem
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Order Order { get; set; } = null!;

    public uint RowVersion { get; set; }
}