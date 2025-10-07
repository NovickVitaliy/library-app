namespace OrderAndInventory.DAL.Models;

public class Payment
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset PaidDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Order Order { get; set; } = null!;
}