using OrderAndInventory.DAL.Database;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.Repositories.Interfaces;

namespace OrderAndInventory.DAL.Repositories.Implementations;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(OrderAndInventoryDbContext dbContext) : base(dbContext)
    {
    }
}