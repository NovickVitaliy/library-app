using OrderAndInventory.BLL.DTOs.OrderItem.Responses;
using OrderAndInventory.BLL.DTOs.Payment.Responses;
using OrderAndInventory.BLL.DTOs.StaffOrder.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.DTOs.Order.Responses;

public record OrderDto(
        Guid OrderId,
        Guid MemberId,
        DateTimeOffset OrderDate,
        OrderStatus Status,
        OrderItemDto[] OrderItems,
        PaymentDto[] Payments,
        StaffOrderDto[] StaffOrders);