using CartAndWishlist.BLL.Features.CartItems.DTOs.Responses;
using CartAndWishlist.Domain.Enums;

namespace CartAndWishlist.BLL.Features.Cart.DTOs.Responses;

public record CartDto(
        Guid CartId,
        Guid MemberId,
        DateTimeOffset CreatedAt,
        CartStatus Status,
        CartItemDto[] CartItems
    );