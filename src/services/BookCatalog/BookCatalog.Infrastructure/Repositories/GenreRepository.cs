using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Database;
using MongoDB.Driver;
using Shared.DTOs;

namespace BookCatalog.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly BookCatalogDbContext _dbContext;
    
    public GenreRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Genre> CreateAsync(Genre genre, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Genres;

        await collection.InsertOneAsync(genre, DatabaseShared.EmptyInsertOneOptions(), cancellationToken);

        return genre;
    }
    
    public async Task<Genre?> GetByIdAsync(Guid genreId, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Genres;

        var filter = Builders<Genre>.Filter.Eq(x => x.GenreId, genreId);

        return await (await collection.FindAsync(filter, cancellationToken: cancellationToken)).SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<PaginationResult<Genre>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Genres;
        
        var sorting = Builders<Genre>.Sort.Ascending(x => x.GenreId);

        var entities = await collection.Find(_ => true)
            .Sort(sorting)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        var totalCount = await CountAsync(cancellationToken);
        
        return PaginationResult<Genre>.Create(
                entities.ToArray(),
                totalCount,
                pageNumber,
                pageSize);
    }
    
    public Task<long> CountAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Genres.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
    }
    
    public async Task<Genre?> UpdateAsync(Guid genreId, Genre genre, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Genres;

        var existingGenre = await (await collection.FindAsync(x => x.GenreId == genreId, cancellationToken: cancellationToken)).SingleOrDefaultAsync(cancellationToken: cancellationToken);

        if (existingGenre is null)
        {
            return null;
        }

        var filter = Builders<Genre>.Filter.Eq(x => x.GenreId, genreId);
        var update = Builders<Genre>.Update
            .Set(x => x.Name, genre.Name)
            .Set(x => x.Description, genre.Description);

        await collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return genre;
    }
    
    public async Task<bool> DeleteAsync(Guid genreId, CancellationToken cancellationToken)
    {
        var collection = _dbContext.Genres;

        var result = await collection.DeleteOneAsync(x => x.GenreId == genreId, cancellationToken);
        
        var books = await (await _dbContext.Books.FindAsync(Builders<Book>.Filter.AnyEq(p => p.GenresIds, genreId), cancellationToken: cancellationToken)).ToListAsync(cancellationToken: cancellationToken);
        foreach (var book in books)
        {
            var bookToUpdate = Builders<Book>.Update.Pull(x => x.GenresIds, genreId);

            await _dbContext.Books.UpdateOneAsync(x => x.BookId == book.BookId, bookToUpdate, cancellationToken: cancellationToken);
        }

        return result.DeletedCount == 1;
    }
}