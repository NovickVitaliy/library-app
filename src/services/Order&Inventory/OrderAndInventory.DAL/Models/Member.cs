namespace OrderAndInventory.DAL.Models;

public class Member
{
    public Guid MemberId { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = [];
}