using System.Linq.Expressions;
using BookCatalog.Domain.Models;
using Shared.DTOs;

namespace BookCatalog.Application.Contracts.Repositories;

public interface IReviewRepository
{
    Task<Review> CreateAsync(Review review, CancellationToken cancellationToken);
    Task<Review?> GetByIdAsync(Guid reviewId, CancellationToken cancellationToken);
    Task<PaginationResult<Review>> GetAsync(Guid bookId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken);
    Task<Review?> UpdateAsync(Guid reviewId, Review review, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid reviewId, CancellationToken cancellationToken);
}