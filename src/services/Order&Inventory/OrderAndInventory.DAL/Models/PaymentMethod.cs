using System.Text.Json.Serialization;

namespace OrderAndInventory.DAL.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentMethod
{
    WayForPay,
    PayPal,
    Privat24,
    Mono
}