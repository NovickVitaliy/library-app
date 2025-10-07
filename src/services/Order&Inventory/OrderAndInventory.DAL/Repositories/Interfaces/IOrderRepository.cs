using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Repositories.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetOrderWithRelatedEntities(Guid id, bool inludeMember, bool includeOrderItems, bool includePayments, bool includeStaffOrders, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetOrdersByMemberId(Guid memberId, int pageSize, int pageNumber, CancellationToken cancellationToken);
}