using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Responses;
using CartAndWishlist.BLL.Features.WishlistItem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartAndWishlist.API.Controllers;

[Route("api/wishlist-items")]
public class WishlistItemController : BaseApiController
{
    private readonly IWishlistItemService _wishlistItemService;
    
    public WishlistItemController(IWishlistItemService wishlistItemService)
    {
        _wishlistItemService = wishlistItemService;
    }

    [HttpGet("{wishlistItemId:guid}")]
    [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(Guid wishlistItemId, CancellationToken cancellationToken)
    {
        return (await _wishlistItemService.GetWishlistItemAsync(new GetWishlistItemByIdRequest(wishlistItemId), cancellationToken)).ToApiResponse();
    }

    [HttpPost]
    [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateWishlistItemRequest request, CancellationToken cancellationToken)
    {
        return (await _wishlistItemService.CreateWishlistItemAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{wishlistItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid wishlistItemId, CancellationToken cancellationToken)
    {
        return (await _wishlistItemService.DeleteWishlistItemAsync(new DeleteWishlistItemRequest(wishlistItemId), cancellationToken)).ToApiResponse();
    }
    
}