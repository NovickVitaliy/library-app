using CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;
using CartAndWishlist.BLL.Features.Wishlist.DTOs.Responses;
using CartAndWishlist.BLL.Features.Wishlist.Services.Interfaces;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Responses;
using CartAndWishlist.BLL.Features.WishlistItem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace CartAndWishlist.API.Controllers;

[Route("api/wishlists")]
public class WishlistController : BaseApiController
{
    private readonly IWishlistService _wishlistService;
    private readonly IWishlistItemService _wishlistItemService;
    
    public WishlistController(IWishlistService wishlistService, IWishlistItemService wishlistItemService)
    {
        _wishlistService = wishlistService;
        _wishlistItemService = wishlistItemService;
    }

    [HttpGet("{wishlistId:guid}")]
    [ProducesResponseType(typeof(WishlistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(Guid wishlistId, CancellationToken cancellationToken)
    {
        return (await _wishlistService.GetWishlistByIdAsync(new GetWishlistRequest(wishlistId), cancellationToken)).ToApiResponse();
    }

    [HttpPost]
    [ProducesResponseType(typeof(WishlistDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateWishlistRequest request, CancellationToken cancellationToken)
    {
        return (await _wishlistService.CreateWishlistAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WishlistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateWishlistRequest request, CancellationToken cancellationToken)
    {
        return (await _wishlistService.UpdateWishlistAsync(id, request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{wishlistId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid wishlistId, CancellationToken cancellationToken)
    {
        return (await _wishlistService.DeleteWishlistAsync(new DeleteWishlistRequest(wishlistId), cancellationToken)).ToApiResponse();
    }

    [HttpGet("{id:guid}/wishlist-items")]
    [ProducesResponseType(typeof(PaginationResult<WishlistItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWishlistItemsAsync(Guid id, [FromQuery] GetWishlistItemsRequest request, CancellationToken cancellationToken)
    {
        return (await _wishlistItemService.GetWishlistItemsAsync(id, request, cancellationToken)).ToApiResponse();
    }
}