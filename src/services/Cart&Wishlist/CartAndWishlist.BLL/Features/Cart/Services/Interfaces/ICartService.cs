using CartAndWishlist.BLL.Features.Cart.DTOs.Requests;
using CartAndWishlist.BLL.Features.Cart.DTOs.Responses;
using Shared.ErrorHandling;

namespace CartAndWishlist.BLL.Features.Cart.Services.Interfaces;

public interface ICartService
{
    Task<Result<CartDto?>> CreateCartAsync(CreateCartRequest request, CancellationToken cancellationToken);
    Task<Result<CartDto?>> GetCartByIdAsync(GetCartByIdRequest request, CancellationToken cancellationToken);
    Task<Result<CartDto?>> GetCartByMemberIdAsync(GetCartByMemberIdRequest request, CancellationToken cancellationToken);
    Task<Result<CartDto?>> UpdateCartAsync(UpdateCartRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteCartAsync(DeleteCartRequest request, CancellationToken cancellationToken);
}