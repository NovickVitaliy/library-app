using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Responses;

namespace CartAndWishlist.BLL.Features.Wishlist.DTOs.Responses;

public record WishlistDto(
    Guid WishlistId,
    Guid MemberId,
    string Name,
    DateTimeOffset CreatedAt,
    WishlistItemDto[] WishlistItems);