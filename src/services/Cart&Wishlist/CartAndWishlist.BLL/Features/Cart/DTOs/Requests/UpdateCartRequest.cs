using System.ComponentModel.DataAnnotations;
using CartAndWishlist.Domain.Enums;

namespace CartAndWishlist.BLL.Features.Cart.DTOs.Requests;

public record UpdateCartRequest(
    [Required(ErrorMessage = "Cart Id is required")] Guid CartId, 
    [Required(ErrorMessage = "Status is required")] CartStatus Status);