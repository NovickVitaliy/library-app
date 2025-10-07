namespace OrderAndInventory.BLL.DTOs.OrderItem.Responses;

public record OrderItemDto(
    Guid OrderItemId,
    Guid OrderId,
    Guid BookId,
    int Quantity,
    decimal UnitPrice);