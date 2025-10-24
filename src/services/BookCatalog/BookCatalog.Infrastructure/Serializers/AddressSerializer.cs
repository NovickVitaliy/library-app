using BookCatalog.Domain.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace BookCatalog.Infrastructure.Serializers;

public class AddressSerializer : SerializerBase<Address>
{
    public override Address Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return Address.Create(value);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Address value)
    {
        context.Writer.WriteString(value.Value);
    }
}