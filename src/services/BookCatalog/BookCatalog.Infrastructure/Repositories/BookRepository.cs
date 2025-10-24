using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Database;
using MongoDB.Driver;
using Shared.DTOs;

namespace BookCatalog.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookCatalogDbContext _dbContext;
    
    public BookRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Book> CreateAsync(Book book, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Books;

        await collection.InsertOneAsync(book, DatabaseShared.EmptyInsertOneOptions(), cancellationToken);

        return book;
    }
    
    public async Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Books;

        var lookupPublishers = PipelineStageDefinitionBuilder.Lookup<Book, Publisher, Book>(
                foreignCollection: _dbContext.Publishers,
                localField: x => x.PublishersIds,
                foreignField: x => x.PublisherId,
                @as: x=> x.Publishers);

        var lookupGenres = PipelineStageDefinitionBuilder.Lookup<Book, Genre, Book>(
                foreignCollection: _dbContext.Genres,
                localField: x=> x.GenresIds,
                foreignField: x=> x.GenreId,
                @as: x => x.Genres);

        var lookupReviews = PipelineStageDefinitionBuilder.Lookup<Book, Review, Book>(
                foreignCollection: _dbContext.Reviews,
                localField: x => x.ReviewsIds,
                foreignField: x => x.ReviewId,
                @as: x => x.Reviews);
        
        var book = await collection
            .Aggregate()
            .Match(Builders<Book>.Filter.Eq(x => x.BookId, bookId))
            .AppendStage(lookupPublishers)
            .AppendStage(lookupGenres)
            .AppendStage(lookupReviews)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return book;
    }
    
    public async Task<PaginationResult<Book>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Books;

        var sorting = Builders<Book>.Sort.Ascending(x => x.BookId);
        var entities = await collection.Find(Builders<Book>.Filter.Empty)
            .Sort(sorting)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await CountAsync(cancellationToken);

        return PaginationResult<Book>.Create(
                entities.ToArray(),
                totalCount,
                pageNumber,
                pageSize);
    }
    
    public Task<long> CountAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Books.CountDocumentsAsync(Builders<Book>.Filter.Empty, cancellationToken: cancellationToken);
    }
    
    public async Task<Book?> UpdateAsync(Guid bookId, Book book, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Books;
        var existingBook = await (await collection.FindAsync(x => x.BookId == bookId, cancellationToken: cancellationToken)).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (existingBook is null)
        {
            return null;
        }
        
        var update = Builders<Book>.Update
            .Set(x => x.Title, book.Title)
            .Set(x => x.Author, book.Author)
            .Set(x => x.Pages, book.Pages)
            .Set(x => x.ShelfLocation, book.ShelfLocation)
            .Set(x => x.Price, book.Price)
            .Set(x => x.Weight, book.Weight)
            .Set(x => x.ShippingCost, book.ShippingCost)
            .Set(x => x.FileFormat, book.FileFormat)
            .Set(x => x.DownloadLink, book.DownloadLink)
            .Set(x => x.Illustrator, book.Illustrator)
            .Set(x => x.Edition, book.Edition);

        await collection.UpdateOneAsync(Builders<Book>.Filter.Eq(x => x.BookId, bookId), update, cancellationToken: cancellationToken);

        return book;
    }
    
    public async Task<bool> DeleteAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Books;

        var deleteResult = await collection.DeleteOneAsync(x => x.BookId == bookId, cancellationToken: cancellationToken);

        return deleteResult.DeletedCount == 1;
    }
}