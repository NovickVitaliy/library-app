namespace OrderAndInventory.DAL.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public Guid MemberId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    
    public Member Member { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
    public ICollection<StaffOrder> StaffOrders { get; set; } = [];

    
    public uint RowVersion { get; set; }
}   