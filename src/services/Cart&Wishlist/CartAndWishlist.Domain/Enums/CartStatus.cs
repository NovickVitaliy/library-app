using System.Text.Json;
using System.Text.Json.Serialization;

namespace CartAndWishlist.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CartStatus
{
    Active,
    CheckedOut
}