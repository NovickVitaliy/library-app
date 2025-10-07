using OrderAndInventory.BLL.DTOs.Inventory.Requests;
using OrderAndInventory.BLL.DTOs.Inventory.Responses;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Interfaces;

public interface IInventoryService
{
    Task<Result<InventoryDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<PaginationResult<InventoryDto>>> GetAsync(GetInventoriesRequest request, CancellationToken cancellationToken);
    Task<Result<InventoryDto?>> CreateAsync(CreateInventoryRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<InventoryDto?>> UpdateAsync(Guid id, UpdateInventoryRequest request, CancellationToken cancellationToken);
}