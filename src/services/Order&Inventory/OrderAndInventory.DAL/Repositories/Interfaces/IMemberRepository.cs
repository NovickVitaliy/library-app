using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Repositories.Interfaces;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<Member?> GetMemberWithOrdersAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Member>> GetMembersWithOrdersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}