using Microsoft.EntityFrameworkCore;
using OrderAndInventory.DAL.Database;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.Repositories.Interfaces;

namespace OrderAndInventory.DAL.Repositories.Implementations;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(OrderAndInventoryDbContext dbContext) : base(dbContext)
    { }
    
    public async Task<Payment?> GetPaymentWithOrder(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Payments
            .Where(x => x.PaymentId == id)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(cancellationToken);
    }
}