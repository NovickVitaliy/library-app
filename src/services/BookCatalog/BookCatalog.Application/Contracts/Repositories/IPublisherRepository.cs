using BookCatalog.Domain.Models;
using Shared.DTOs;

namespace BookCatalog.Application.Contracts.Repositories;

public interface IPublisherRepository
{
    Task<Publisher> CreateAsync(Publisher publisher, CancellationToken cancellationToken);
    Task<Publisher?> GetByIdAsync(Guid publisherId, CancellationToken cancellationToken);
    Task<PaginationResult<Publisher>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAsync(CancellationToken cancellationToken);
    Task<Publisher?> UpdateAsync(Guid publisherId, Publisher publisher, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid publisherId, CancellationToken cancellationToken);
}