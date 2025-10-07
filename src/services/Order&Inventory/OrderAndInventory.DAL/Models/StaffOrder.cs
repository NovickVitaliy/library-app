namespace OrderAndInventory.DAL.Models;

public class StaffOrder
{
    public Guid StaffId { get; set; }
    public Guid OrderId { get; set; }

    public DateTimeOffset AssignedAt { get; set; } = DateTimeOffset.UtcNow;
    
    public Staff Staff { get; set; } = null!;
    public Order Order { get; set; } = null!;
}