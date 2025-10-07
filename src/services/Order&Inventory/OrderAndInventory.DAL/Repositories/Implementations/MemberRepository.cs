using Microsoft.EntityFrameworkCore;
using OrderAndInventory.DAL.Database;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.Repositories.Interfaces;

namespace OrderAndInventory.DAL.Repositories.Implementations;

public class MemberRepository : GenericRepository<Member>, IMemberRepository
{
    public MemberRepository(OrderAndInventoryDbContext dbContext) : base(dbContext)
    {
    }


    public Task<Member?> GetMemberWithOrdersAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Members
            .Where(x => x.MemberId == id)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Member>> GetMembersWithOrdersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _dbContext.Members
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Orders)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return _dbContext.Members.Where(x => x.Email == email).SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
}