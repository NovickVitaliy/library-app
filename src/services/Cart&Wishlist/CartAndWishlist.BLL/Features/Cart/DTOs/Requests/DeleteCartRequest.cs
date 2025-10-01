using System.ComponentModel.DataAnnotations;

namespace CartAndWishlist.BLL.Features.Cart.DTOs.Requests;

public record DeleteCartRequest([Required(ErrorMessage = "Cart Id is required")] Guid CartId);