namespace OrderAndInventory.DAL.Models;

public class Staff
{
    public Guid StaffId { get; set; }
    public string Name { get; set; } = null!;
    public string Role { get; set; } = null!;

    public ICollection<StaffOrder> StaffOrders { get; set; } = [];
}