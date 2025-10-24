using System.Linq.Expressions;
using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Database;
using MongoDB.Driver;
using Shared.DTOs;

namespace BookCatalog.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly BookCatalogDbContext _dbContext;

    public ReviewRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Review> CreateAsync(Review review, CancellationToken cancellationToken)
    {
        await _dbContext.Reviews.InsertOneAsync(review, DatabaseShared.EmptyInsertOneOptions(), cancellationToken);

        var book = await (await _dbContext.Books.FindAsync(x => x.BookId == review.BookId, cancellationToken: cancellationToken)).SingleAsync(cancellationToken: cancellationToken);
        
        book.ReviewsIds.Add(review.ReviewId);

        await _dbContext.Books.UpdateOneAsync(x => x.BookId == review.BookId, Builders<Book>.Update.Set(x => x.ReviewsIds, book.ReviewsIds), cancellationToken: cancellationToken);
        
        return review;
    }

    public async Task<Review?> GetByIdAsync(Guid reviewId, CancellationToken cancellationToken)
    {
        return await (await _dbContext.Reviews.FindAsync(Builders<Review>.Filter.Eq(x => x.ReviewId, reviewId), cancellationToken: cancellationToken))
            .SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task<PaginationResult<Review>> GetAsync(Guid bookId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        Expression<Func<Review, bool>> predicate = x => x.BookId == bookId;
        var entities = await _dbContext.Reviews
            .Find(predicate)
            .Sort(Builders<Review>.Sort.Ascending(x => x.ReviewId))
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        var totalCount = await CountAsync(predicate, cancellationToken);
        
        return PaginationResult<Review>.Create(
                entities.ToArray(),
                totalCount,
                pageNumber,
                pageSize);
    }
    
    public Task<long> CountAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken)
    {
        return _dbContext.Reviews.Find(predicate).CountDocumentsAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<Review?> UpdateAsync(Guid reviewId, Review review, CancellationToken cancellationToken)
    {
        var filter = Builders<Review>.Filter.Eq(x => x.ReviewId, reviewId);

        var update = Builders<Review>.Update
            .Set(x => x.Rating, review.Rating)
            .Set(x => x.Text, review.Text);

        await _dbContext.Reviews.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return review;
    }
    
    public async Task<bool> DeleteAsync(Guid reviewId, CancellationToken cancellationToken)
    {
        var deletionResult = await _dbContext.Reviews.DeleteOneAsync(x => x.ReviewId == reviewId, cancellationToken);

        return deletionResult.DeletedCount == 1;
    }
}