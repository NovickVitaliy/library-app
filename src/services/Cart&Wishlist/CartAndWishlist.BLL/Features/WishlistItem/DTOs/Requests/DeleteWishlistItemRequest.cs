using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;

public record DeleteWishlistItemRequest(
    [Required(ErrorMessage = "Wishlist Item Id is required")] Guid WishlistItemId);