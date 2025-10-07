using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Repositories.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<Payment?> GetPaymentWithOrder(Guid id, CancellationToken cancellationToken);
}