using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookCatalog.Domain.Models;

public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid ReviewId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public decimal Rating { get; set; }
    public string Text { get; set; } = null!;
    public DateTimeOffset CreatedDate { get; set; }
}