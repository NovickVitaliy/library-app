using BookCatalog.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookCatalog.Domain.Models;

public class Publisher
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid PublisherId { get; set; }
    public string Name { get; set; } = null!;
    public Address Address { get; set; } = null!;

    public Guid[] BooksIds { get; set; } = [];
    [BsonIgnore] public Book[] Books { get; set; } = [];
}