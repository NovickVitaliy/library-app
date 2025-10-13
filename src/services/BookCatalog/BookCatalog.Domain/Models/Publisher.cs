using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookCatalog.Domain.Models;

public class Publisher
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid PublisherId { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}