using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;

public record GetWishlistByMemberIdRequest(
    [Required(ErrorMessage = "Member Id is required")] Guid MemberId);