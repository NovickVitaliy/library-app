using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;

public record UpdateWishlistRequest(
    [Required(ErrorMessage = "Name is required")] [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")] string Name);