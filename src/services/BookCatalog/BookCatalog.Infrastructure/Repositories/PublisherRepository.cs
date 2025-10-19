using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Database;
using MongoDB.Driver;
using Shared.DTOs;

namespace BookCatalog.Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly BookCatalogDbContext _dbContext;
    
    public PublisherRepository(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Publisher> CreateAsync(Publisher publisher, CancellationToken cancellationToken)
    {
        await _dbContext.Publishers.InsertOneAsync(publisher, DatabaseShared.EmptyInsertOneOptions(), cancellationToken);

        return publisher;
    }
    
    public async Task<Publisher?> GetByIdAsync(Guid publisherId, CancellationToken cancellationToken)
    {
        var filter = Builders<Publisher>.Filter.Eq(x => x.PublisherId, publisherId);

        return await (await _dbContext.Publishers.FindAsync(filter, cancellationToken: cancellationToken)).SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task<PaginationResult<Publisher>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var sorting = Builders<Publisher>.Sort.Ascending(x => x.PublisherId);

        var entities = await _dbContext.Publishers.Find(_ => true)
            .Sort(sorting)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        var totalCount = await CountAsync(cancellationToken);

        return PaginationResult<Publisher>.Create(
                entities.ToArray(),
                totalCount,
                pageNumber,
                pageSize);
    }
    
    public Task<long> CountAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Publishers.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
    }
    
    public async Task<Publisher?> UpdateAsync(Guid publisherId, Publisher publisher, CancellationToken cancellationToken)
    {
        var filter = Builders<Publisher>.Filter.Eq(x => x.PublisherId, publisherId);

        var update = Builders<Publisher>.Update
            .Set(x => x.Name, publisher.Name)
            .Set(x => x.Address, publisher.Address);

        await _dbContext.Publishers.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return publisher;
    }
    
    public async Task<bool> DeleteAsync(Guid publisherId, CancellationToken cancellationToken)
    {
        var deletionResult = await _dbContext.Publishers.DeleteOneAsync(x => x.PublisherId == publisherId, cancellationToken);

        return deletionResult.DeletedCount == 1;
    }
}