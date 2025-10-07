using Microsoft.EntityFrameworkCore;
using OrderAndInventory.DAL.Database;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.Repositories.Interfaces;

namespace OrderAndInventory.DAL.Repositories.Implementations;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(OrderAndInventoryDbContext dbContext) : base(dbContext)
    { }
    
    public async Task<Order?> GetOrderWithRelatedEntities(Guid id, bool inludeMember, bool includeOrderItems, bool includePayments, bool includeStaffOrders, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(x => x.OrderId == id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (order is null) return null;

        if (inludeMember)
        {
            await _dbContext.Entry(order)
                .Reference(x => x.Member)
                .LoadAsync(cancellationToken);
        }

        if (includeOrderItems)
        {
            await _dbContext.Entry(order)
                .Collection(x => x.OrderItems)
                .LoadAsync(cancellationToken);
        }

        if (includePayments)
        {
            await _dbContext.Entry(order)
                .Collection(x => x.Payments)
                .LoadAsync(cancellationToken);
        }

        if (includeStaffOrders)
        {
            await _dbContext.Entry(order)
                .Collection(x => x.StaffOrders)
                .LoadAsync(cancellationToken);
        }

        return order;
    }
    
    public async Task<IEnumerable<Order>> GetOrdersByMemberId(Guid memberId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Where(x => x.MemberId == memberId)
            .Include(x => x.Member)
            .Include(x => x.OrderItems)
            .Include(x => x.Payments)
            .Include(x => x.StaffOrders)
            .AsSplitQuery()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}