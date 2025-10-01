using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;

public record UpdateCartItemRequest(
    [Required(ErrorMessage = "Quantity is required")] [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value")] int Quantity);