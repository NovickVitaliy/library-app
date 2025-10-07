using OrderAndInventory.BLL.DTOs.Staff.Requests;
using OrderAndInventory.BLL.DTOs.Staff.Responses;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Interfaces;

public interface IStaffService
{
    Task<Result<StaffDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<PaginationResult<StaffDto>>> GetAsync(GetStavesRequest request, CancellationToken cancellationToken);
    Task<Result<StaffDto?>> CreateAsync(CreateStaffRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<StaffDto?>> UpdateAsync(Guid id, UpdateStaffRequest request, CancellationToken cancellationToken);
}