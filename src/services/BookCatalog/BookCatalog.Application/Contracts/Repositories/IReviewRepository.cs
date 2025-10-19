using BookCatalog.Domain.Models;
using Shared.DTOs;

namespace BookCatalog.Application.Contracts.Repositories;

public interface IReviewRepository
{
    Task<Review> CreateAsync(Review review, CancellationToken cancellationToken);
    Task<Review?> GetByIdAsync(Guid reviewId, CancellationToken cancellationToken);
    Task<PaginationResult<Review>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAsync(CancellationToken cancellationToken);
    Task<Review?> UpdateAsync(Guid reviewId, Review review, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid reviewId, CancellationToken cancellationToken);
}