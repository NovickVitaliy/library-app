using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.DTOs.Payment.Responses;

public record PaymentDto(
        Guid PaymentId,
        Guid OrderId,
        decimal Amount,
        DateTimeOffset PaidDate,
        PaymentMethod PaymentMethod);