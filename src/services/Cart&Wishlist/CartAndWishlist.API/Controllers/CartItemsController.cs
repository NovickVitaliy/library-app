using CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Responses;
using CartAndWishlist.BLL.Features.CartItems.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartAndWishlist.API.Controllers;

[Route("api/cart-items")]
public class CartItemsController : BaseApiController
{
    private readonly ICartItemsService _cartItemsService;
    
    public CartItemsController(ICartItemsService cartItemsService)
    {
        _cartItemsService = cartItemsService;
    }

    [HttpGet("{cartItemId:guid}")]
    [ProducesResponseType(typeof(CartItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(Guid cartItemId, CancellationToken cancellationToken)
    {
        return (await _cartItemsService.GetCartItemAsync(new GetCartItemRequest(cartItemId), cancellationToken)).ToApiResponse();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CartItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateCartItemRequest request, CancellationToken cancellationToken)
    {
        return (await _cartItemsService.CreateCartItemAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CartItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateCartItemRequest request, CancellationToken cancellationToken)
    {
        return (await _cartItemsService.UpdateCartItemAsync(id, request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{cartItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid cartItemId, CancellationToken cancellationToken)
    {
        return (await _cartItemsService.DeleteCartItemAsync(new DeleteCartItemRequest(cartItemId), cancellationToken)).ToApiResponse();
    }
}