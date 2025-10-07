using System.Net;
using CartAndWishlist.BLL.Features.Cart.DTOs.Requests;
using CartAndWishlist.BLL.Features.Cart.DTOs.Responses;
using CartAndWishlist.BLL.Features.Cart.Services.Interfaces;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Responses;
using CartAndWishlist.BLL.Features.CartItems.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace CartAndWishlist.API.Controllers;

[Route("api/carts")]
public class CartController : BaseApiController
{
    private readonly ICartService _cartService;
    private readonly ICartItemsService _cartItemsService;
    
    public CartController(ICartService cartService, ICartItemsService cartItemsService)
    {
        _cartService = cartService;
        _cartItemsService = cartItemsService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostAsync(CreateCartRequest request, CancellationToken cancellationToken)
    {
        return (await _cartService.CreateCartAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet("{cartId:guid}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid cartId, CancellationToken cancellationToken)
    {
        return (await _cartService.GetCartByIdAsync(new GetCartByIdRequest(cartId), cancellationToken)).ToApiResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateCartRequest request, CancellationToken cancellationToken)
    {
        request = request with { CartId = id };
        return (await _cartService.UpdateCartAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid cartId, CancellationToken cancellationToken)
    {
        return (await _cartService.DeleteCartAsync(new DeleteCartRequest(cartId), cancellationToken)).ToApiResponse();
    }

    [HttpGet("{cartId:guid}/cart-items")]
    [ProducesResponseType(typeof(PaginationResult<CartItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartItemsAsync(Guid cartId, [FromQuery] GetCartItemsRequest request, CancellationToken cancellationToken)
    {
        return (await _cartItemsService.GetCartItemsAsync(cartId, request, cancellationToken)).ToApiResponse();
    }
}