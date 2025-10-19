using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Infrastructure.Database;
using BookCatalog.Infrastructure.Options;
using BookCatalog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        
        services.AddOptions<MongoOptions>()
            .BindConfiguration(MongoOptions.ConfigurationKey)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>();
            
            return new MongoClient(options.Value.ConnectionString);
        });

        services.AddScoped<BookCatalogDbContext>();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        
        return services;
    }
}