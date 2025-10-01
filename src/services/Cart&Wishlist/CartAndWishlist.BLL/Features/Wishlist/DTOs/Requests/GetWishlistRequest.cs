using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;

public record GetWishlistRequest(
    [Required(ErrorMessage = "Wishlist Id is required")] Guid WishlistId);