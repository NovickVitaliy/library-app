namespace CartAndWishlist.BLL.Features.WishlistItem.DTOs.Responses;

public record WishlistItemDto(
    Guid WishlistItemId,
    Guid WishlistId,
    Guid BookId,
    DateTimeOffset AddedAt);