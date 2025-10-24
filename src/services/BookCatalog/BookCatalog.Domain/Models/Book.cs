using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookCatalog.Domain.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid BookId { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public int Pages { get; set; }
    public string? ShelfLocation { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? Weight { get; set; }
    public decimal ShippingCost { get; set; }
    public string? FileFormat { get; set; }
    public string? DownloadLink { get; set; }
    public string? Illustrator { get; set; }
    public string? Edition { get; set; }

   [BsonIgnoreIfDefault] public Genre[] Genres { get; set; } = [];
   [BsonIgnoreIfDefault] public Publisher[] Publishers { get; set; } = [];
   [BsonIgnoreIfDefault] public Review[] Reviews { get; set; } = [];
    
    public List<Guid> GenresIds { get; set; } = [];
    public List<Guid> PublishersIds { get; set; } = [];
    public List<Guid> ReviewsIds { get; set; } = [];
}