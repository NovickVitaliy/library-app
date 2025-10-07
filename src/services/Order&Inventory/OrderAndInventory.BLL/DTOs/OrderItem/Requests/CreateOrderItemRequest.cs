using System.ComponentModel.DataAnnotations;

namespace OrderAndInventory.BLL.DTOs.OrderItem.Requests;

public record CreateOrderItemRequest(
        [Required(ErrorMessage = "Book Id is required")]Guid BookId,
        [Required(ErrorMessage = "Quantity is required")] [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive value")] int Quantity,
        [Required(ErrorMessage = "Unit Price is required")] [Range(0, int.MaxValue, ErrorMessage = "Unit Price must be a positive value")] decimal UnitPrice);