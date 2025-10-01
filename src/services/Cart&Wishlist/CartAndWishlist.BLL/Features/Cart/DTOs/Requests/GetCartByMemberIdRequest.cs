using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.Cart.DTOs.Requests;

public record GetCartByMemberIdRequest([Required(ErrorMessage = "Member Id is required")] Guid MemberId);