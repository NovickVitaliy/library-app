using System.ComponentModel.DataAnnotations;
using OrderAndInventory.BLL.DTOs.OrderItem.Requests;
using OrderAndInventory.BLL.DTOs.StaffOrder.Requests;
using Shared.DTOs;

namespace OrderAndInventory.BLL.DTOs.Order.Requests;

public record CreateOrderRequest(
        [Required(ErrorMessage = "Member Id is required")] Guid MemberId,
        CreateOrderItemRequest[] OrderItems,
        CreateStaffOrderRequest[] StaffOrders);