using BookCatalog.Domain.Models;
using Shared.DTOs;

namespace BookCatalog.Application.Contracts.Repositories;

public interface IGenreRepository
{
    Task<Genre> CreateAsync(Genre genre, CancellationToken cancellationToken);
    Task<Genre?> GetByIdAsync(Guid genreId, CancellationToken cancellationToken);
    Task<PaginationResult<Genre>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAsync(CancellationToken cancellationToken);
    Task<Genre?> UpdateAsync(Guid genreId, Genre genre, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid genreId, CancellationToken cancellationToken);
}