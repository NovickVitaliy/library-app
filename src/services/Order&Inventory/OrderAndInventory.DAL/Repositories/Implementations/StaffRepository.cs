using Microsoft.EntityFrameworkCore;
using OrderAndInventory.DAL.Database;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.Repositories.Interfaces;

namespace OrderAndInventory.DAL.Repositories.Implementations;

public class StaffRepository : GenericRepository<Staff>, IStaffRepository
{
    public StaffRepository(OrderAndInventoryDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<Staff?> GetStaffWithOrders(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Staves
            .Where(x => x.StaffId == id)
            .Include(x => x.StaffOrders)
            .ThenInclude(x => x.Order)
            .FirstOrDefaultAsync(cancellationToken);
    }
}