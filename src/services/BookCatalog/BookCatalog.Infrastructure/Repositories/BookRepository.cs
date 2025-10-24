using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Database;
using MongoDB.Bson;
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

        List<Task<UpdateResult>> tasks = [];
        tasks.AddRange(book.PublishersIds.Select(publisherId => _dbContext.Publishers.UpdateOneAsync(x => x.PublisherId == publisherId,
            Builders<Publisher>.Update.AddToSet(x => x.BooksIds, book.BookId), cancellationToken: cancellationToken)));

        await Task.WhenAll(tasks);

        return book;
    }

    public async Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Books;

        var book = await collection
            .Aggregate()
            .Match(Builders<Book>.Filter.Eq(x => x.BookId, bookId))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        book.Genres =
            (await (await _dbContext.Genres.FindAsync(Builders<Genre>.Filter.In(x => x.GenreId, book.GenresIds), cancellationToken: cancellationToken)).ToListAsync(
                cancellationToken: cancellationToken)).ToArray();
        book.Publishers = (await (await _dbContext.Publishers.FindAsync(Builders<Publisher>.Filter.In(x => x.PublisherId, book.PublishersIds), cancellationToken: cancellationToken))
            .ToListAsync(cancellationToken: cancellationToken)).ToArray();
        book.Reviews =
            (await (await _dbContext.Reviews.FindAsync(Builders<Review>.Filter.In(x => x.ReviewId, book.ReviewsIds), cancellationToken: cancellationToken)).ToListAsync(
                cancellationToken: cancellationToken)).ToArray();

        var publishers = await (await _dbContext.Publishers.FindAsync(Builders<Publisher>.Filter.In(x => x.PublisherId, book.PublishersIds), cancellationToken: cancellationToken))
            .ToListAsync(cancellationToken: cancellationToken);
        foreach (var publisher in publishers)
        {
            var update = Builders<Publisher>.Update.AddToSet(x => x.BooksIds, bookId);

            await _dbContext.Publishers.UpdateOneAsync(x => x.PublisherId == publisher.PublisherId, update, cancellationToken: cancellationToken);
        }

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
            .Set(x => x.Edition, book.Edition)
            .Set(x => x.GenresIds, book.GenresIds)
            .Set(x => x.ReviewsIds, book.ReviewsIds)
            .Set(x => x.PublishersIds, book.PublishersIds);

        var publishers = await (await _dbContext.Publishers.FindAsync(Builders<Publisher>.Filter.In(x => x.PublisherId, book.PublishersIds), cancellationToken: cancellationToken))
            .ToListAsync(cancellationToken: cancellationToken);
        foreach (var publisher in publishers)
        {
            var publisherUpdate = Builders<Publisher>.Update.AddToSet(x => x.BooksIds, bookId);

            await _dbContext.Publishers.UpdateOneAsync(x => x.PublisherId == publisher.PublisherId, publisherUpdate, cancellationToken: cancellationToken);
        }


        await collection.UpdateOneAsync(Builders<Book>.Filter.Eq(x => x.BookId, bookId), update, cancellationToken: cancellationToken);

        return book;
    }

    public async Task<bool> DeleteAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Books;

        var deleteResult = await collection.DeleteOneAsync(x => x.BookId == bookId, cancellationToken: cancellationToken);

        var publishers =
            await (await _dbContext.Publishers.FindAsync(Builders<Publisher>.Filter.AnyEq(p => p.BooksIds, bookId), cancellationToken: cancellationToken)).ToListAsync(
                cancellationToken: cancellationToken);
        foreach (var publisher in publishers)
        {
            var publisherUpdate = Builders<Publisher>.Update.Pull(x => x.BooksIds, bookId);

            await _dbContext.Publishers.UpdateOneAsync(x => x.PublisherId == publisher.PublisherId, publisherUpdate, cancellationToken: cancellationToken);
        }

        return deleteResult.DeletedCount == 1;
    }
}