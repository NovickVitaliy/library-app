using System.Text.Json.Serialization;

namespace OrderAndInventory.DAL.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending, // when we only created it 
    Processing, // when we paid for it
    Shipped,
    Delivered,
    Cancelled
}