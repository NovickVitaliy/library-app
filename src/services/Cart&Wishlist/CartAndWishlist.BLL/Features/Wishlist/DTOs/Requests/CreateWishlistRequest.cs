using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;

public record CreateWishlistRequest(
    [Required(ErrorMessage = "Member Id is required")] 
    Guid MemberId, 
    
    [Required(ErrorMessage = "Name is required")] 
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")] 
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
    string Name);