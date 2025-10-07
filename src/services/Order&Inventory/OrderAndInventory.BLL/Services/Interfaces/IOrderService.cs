using OrderAndInventory.BLL.DTOs.Order.Requests;
using OrderAndInventory.BLL.DTOs.Order.Responses;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Interfaces;

public interface IOrderService
{
    Task<Result<OrderDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<PaginationResult<OrderDto>>> GetAsync(GetOrdersRequest request, CancellationToken cancellationToken);
    Task<Result<OrderDto?>> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
}