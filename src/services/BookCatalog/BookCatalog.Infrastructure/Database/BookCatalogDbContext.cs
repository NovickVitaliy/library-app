using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookCatalog.Infrastructure.Database;

public class BookCatalogDbContext
{
    private readonly IMongoDatabase _mongoDatabase;
    private const string BooksCollectionName = "Books";
    private const string GenresCollectionName = "Genres";
    private const string PublishersCollectionName = "Publisher";
    private const string ReviewsCollectionName = "Reviews";
    
    public BookCatalogDbContext(IMongoClient mongoClient, IOptions<MongoOptions> options)
    {
        _mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<Book> Books => _mongoDatabase.GetCollection<Book>(BooksCollectionName);
    public IMongoCollection<Genre> Genres => _mongoDatabase.GetCollection<Genre>(GenresCollectionName);
    public IMongoCollection<Publisher> Publishers => _mongoDatabase.GetCollection<Publisher>(PublishersCollectionName);
    public IMongoCollection<Review> Reviews => _mongoDatabase.GetCollection<Review>(ReviewsCollectionName);
}