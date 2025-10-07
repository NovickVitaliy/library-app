using OrderAndInventory.BLL.DTOs.Common;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.DTOs.Payment.Requests;

public record GetPaymentsRequest(
        decimal? MinAmount,
        decimal? MaxAmount,
        DateTimeOffset? MinPaidDate,
        DateTimeOffset? MaxPaidDate,
        PaymentMethod? PaymentMethod) : GetRequest;