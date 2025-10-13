using BookCatalog.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BookCatalog.Infrastructure.Database;

public static class DbSetup
{
    public static async Task SetupDatabase(this WebApplication app)
    {
        ConfigureConventions();
        await SeedCollections(app);
    }
    
    private static async Task SeedCollections(WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookCatalogDbContext>();

        if (!await dbContext.Genres.Find(_ => true).AnyAsync())
        {
            await dbContext.Genres.InsertManyAsync([
                new Genre { GenreId = Guid.NewGuid(), Name = "Science Fiction", Description = "Futuristic and scientific concepts." },
                new Genre { GenreId = Guid.NewGuid(), Name = "Fantasy", Description = "Magical worlds and adventures." },
                new Genre { GenreId = Guid.NewGuid(), Name = "Mystery", Description = "Suspense and investigation stories." }
            ]);
        }

        if (!await dbContext.Publishers.Find(_ => true).AnyAsync())
        {
            await dbContext.Publishers.InsertManyAsync([
                new Publisher { PublisherId = Guid.NewGuid(), Name = "Penguin Books", Address = "80 Strand, London, UK" },
                new Publisher { PublisherId = Guid.NewGuid(), Name = "HarperCollins", Address = "195 Broadway, New York, USA" },
                new Publisher { PublisherId = Guid.NewGuid(), Name = "Macmillan Publishers", Address = "120 Broadway, New York, USA" }
            ]);
        }

        if (!await dbContext.Books.Find(_ => true).AnyAsync())
        {
            await dbContext.Books.InsertManyAsync([
                new Book
                {
                    BookId = Guid.NewGuid(),
                    Title = "Dune",
                    Author = "Frank Herbert",
                    Pages = 688,
                    ShelfLocation = "A1",
                    Price = 24.99m,
                    Weight = 0.8m,
                    ShippingCost = 3.5m,
                    FileFormat = "epub",
                    DownloadLink = "https://example.com/dune",
                    Illustrator = "John Schoenherr",
                    Edition = "Special 50th Anniversary Edition"
                },
                new Book
                {
                    BookId = Guid.NewGuid(),
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Pages = 310,
                    ShelfLocation = "B2",
                    Price = 19.99m,
                    Weight = 0.6m,
                    ShippingCost = 3.0m,
                    FileFormat = "pdf",
                    DownloadLink = "https://example.com/hobbit",
                    Illustrator = "Alan Lee",
                    Edition = "Illustrated Edition"
                }
            ]);
        }

        if (!await dbContext.Reviews.Find(_ => true).AnyAsync())
        {
            var book = await dbContext.Books.Find(_ => true).FirstOrDefaultAsync();

            if (book != null)
            {
                await dbContext.Reviews.InsertManyAsync([
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        BookId = book.BookId,
                        UserId = Guid.NewGuid(),
                        Rating = 4.8m,
                        Text = "An absolutely captivating story with deep lore.",
                        CreatedDate = DateTimeOffset.UtcNow.AddDays(-3)
                    },
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        BookId = book.BookId,
                        UserId = Guid.NewGuid(),
                        Rating = 4.5m,
                        Text = "Amazing world-building and characters!",
                        CreatedDate = DateTimeOffset.UtcNow.AddDays(-1)
                    }
                ]);
            }
        }
    }

    private static void ConfigureConventions()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }
}