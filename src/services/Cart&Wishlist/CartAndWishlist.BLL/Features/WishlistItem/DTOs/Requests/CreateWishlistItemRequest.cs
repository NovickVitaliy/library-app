using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;

public record CreateWishlistItemRequest(
    [Required(ErrorMessage = "Wishlist Id is required")] Guid WishlistId, 
    [Required(ErrorMessage = "Book Id is required")] Guid BookId);