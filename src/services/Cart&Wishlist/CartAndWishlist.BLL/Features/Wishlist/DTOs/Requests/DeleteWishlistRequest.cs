using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;

public record DeleteWishlistRequest(
    [Required(ErrorMessage = "Wishlist Id is required")] Guid WishlistId);