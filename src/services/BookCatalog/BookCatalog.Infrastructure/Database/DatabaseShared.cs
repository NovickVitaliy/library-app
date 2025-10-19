using MongoDB.Driver;

namespace BookCatalog.Infrastructure.Database;

public static class DatabaseShared
{
    private static readonly Lazy<InsertOneOptions> LazyInsertOneOptions = new Lazy<InsertOneOptions>(() => new InsertOneOptions());
    
    public static InsertOneOptions EmptyInsertOneOptions()
    {
        return LazyInsertOneOptions.Value;
    }
}