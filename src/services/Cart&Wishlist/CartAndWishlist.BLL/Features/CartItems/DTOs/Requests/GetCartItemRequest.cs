using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;

public record GetCartItemRequest(
    [Required(ErrorMessage = "Cart Item Id is required")] Guid CartItemId);