using CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Responses;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace CartAndWishlist.BLL.Features.CartItems.Services.Interfaces;

public interface ICartItemsService
{
    Task<Result<CartItemDto>> GetCartItemAsync(GetCartItemRequest request, CancellationToken cancellationToken);
    Task<Result<CartItemDto>> CreateCartItemAsync(CreateCartItemRequest request, CancellationToken cancellationToken);
    Task<Result<CartItemDto>> UpdateCartItemAsync(Guid cartItemId, UpdateCartItemRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteCartItemAsync(DeleteCartItemRequest request, CancellationToken cancellationToken);
    Task<Result<PaginationResult<CartItemDto>>> GetCartItemsAsync(Guid id ,GetCartItemsRequest request, CancellationToken cancellationToken);
}