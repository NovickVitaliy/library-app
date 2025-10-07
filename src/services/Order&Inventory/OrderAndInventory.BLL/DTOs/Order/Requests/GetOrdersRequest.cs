using OrderAndInventory.BLL.DTOs.Common;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.DTOs.Order.Requests;

public record GetOrdersRequest(
        DateTimeOffset? MinOrderDate,
        DateTimeOffset? MaxOrderDate,
        OrderStatus? Status
    ) : GetRequest;