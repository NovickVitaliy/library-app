using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;

public record CreateCartItemRequest(
    [Required(ErrorMessage = "Cart Id is required")] Guid CartId,
    [Required(ErrorMessage = "Book Id is required")] Guid BookId,
    [Required(ErrorMessage = "Quantity is required")] [Range(1, 100, ErrorMessage = "Quantity must be in range from 1 to 100")] int Quantity);