namespace CartAndWishlist.BLL.Features.CartItems.DTOs.Responses;

public record CartItemDto(
    Guid CartItemId,
    Guid CartId,
    Guid BookId,
    int Quantity,
    DateTimeOffset AddedAt);