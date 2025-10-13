using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookCatalog.Domain.Models;

public class Genre
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid GenreId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}