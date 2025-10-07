using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Repositories.Interfaces;

public interface IStaffRepository : IGenericRepository<Staff>
{
    Task<Staff?> GetStaffWithOrders(Guid id, CancellationToken cancellationToken);
}