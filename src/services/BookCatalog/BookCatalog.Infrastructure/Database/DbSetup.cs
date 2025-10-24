using BookCatalog.Domain.Models;
using BookCatalog.Domain.ValueObjects;
using BookCatalog.Infrastructure.Serializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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

        // Seed Genres
        if (!await dbContext.Genres.Find(_ => true).AnyAsync())
        {
            var genres = new[]
            {
                new Genre { GenreId = Guid.NewGuid(), Name = "Science Fiction", Description = "Futuristic and scientific concepts." },
                new Genre { GenreId = Guid.NewGuid(), Name = "Fantasy", Description = "Magical worlds and adventures." },
                new Genre { GenreId = Guid.NewGuid(), Name = "Mystery", Description = "Suspense and investigation stories." }
            };

            await dbContext.Genres.InsertManyAsync(genres);
        }

        // Seed Publishers
        if (!await dbContext.Publishers.Find(_ => true).AnyAsync())
        {
            var publishers = new[]
            {
                new Publisher { PublisherId = Guid.NewGuid(), Name = "Penguin Books", Address = Address.Create("80 Strand, London, UK") },
                new Publisher { PublisherId = Guid.NewGuid(), Name = "HarperCollins", Address = Address.Create("195 Broadway, New York, USA") },
                new Publisher { PublisherId = Guid.NewGuid(), Name = "Macmillan Publishers", Address = Address.Create("120 Broadway, New York, USA") }
            };

            await dbContext.Publishers.InsertManyAsync(publishers);
        }

        // Seed Books
        if (!await dbContext.Books.Find(_ => true).AnyAsync())
        {
            var genres = await dbContext.Genres.Find(_ => true).ToListAsync();
            var publishers = await dbContext.Publishers.Find(_ => true).ToListAsync();

            var book1 = new Book
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
                Edition = "Special 50th Anniversary Edition",
                GenresIds = [genres[0].GenreId],
                PublishersIds = [publishers[0].PublisherId]
            };

            var book2 = new Book
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
                Edition = "Illustrated Edition",
                GenresIds = [genres[1].GenreId],
                PublishersIds = [publishers[1].PublisherId]
            };

            await dbContext.Books.InsertManyAsync([book1, book2]);
        }

        // Seed Reviews
        if (!await dbContext.Reviews.Find(_ => true).AnyAsync())
        {
            var dune = await dbContext.Books.Find(b => b.Title == "Dune").FirstOrDefaultAsync();
            if (dune != null)
            {
                var reviews = new[]
                {
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        BookId = dune.BookId,
                        UserId = Guid.NewGuid(),
                        Rating = 4.8m,
                        Text = "An absolutely captivating story with deep lore.",
                        CreatedDate = DateTimeOffset.UtcNow.AddDays(-3)
                    },
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        BookId = dune.BookId,
                        UserId = Guid.NewGuid(),
                        Rating = 4.5m,
                        Text = "Amazing world-building and characters!",
                        CreatedDate = DateTimeOffset.UtcNow.AddDays(-1)
                    }
                };

                await dbContext.Reviews.InsertManyAsync(reviews);

                dune.ReviewsIds = reviews.Select(r => r.ReviewId).ToList();
                await dbContext.Books.ReplaceOneAsync(b => b.BookId == dune.BookId, dune);
            }
        }
    }

    private static void ConfigureConventions()
    {
        BsonSerializer.RegisterSerializer(new AddressSerializer());
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));
    }
}
