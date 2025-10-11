using AutoMapper;
using Microsoft.Extensions.Logging;
using OrderAndInventory.BLL.DTOs.Staff.Requests;
using OrderAndInventory.BLL.DTOs.Staff.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using OrderAndInventory.BLL.Specifications;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.UOW.Interfaces;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Implenentations;

public class StaffService : IStaffService
{
    private readonly ILogger<StaffService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StaffService(ILogger<StaffService> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<StaffDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching staff with ID: {StaffId}", id);
        var staff = await _unitOfWork.StaffRepository.GetByIdAsync(id, cancellationToken);
        if (staff is null)
        {
            _logger.LogWarning("Staff with ID {StaffId} not found", id);
            return Result<StaffDto?>.NotFound(key: id, entityName: nameof(Staff));
        }
        _logger.LogInformation("Staff with ID {StaffId} retrieved successfully", id);
        return Result<StaffDto?>.Ok(_mapper.Map<StaffDto>(staff));
    }

    public async Task<Result<PaginationResult<StaffDto>>> GetAsync(GetStavesRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching staffs with filters: {@Request}", request);
        var spec = new StaffSpecification(request);
        var staffs = (await _unitOfWork.StaffRepository.ListBySpecAsync(spec, cancellationToken)).ToList();
        var totalCount = await _unitOfWork.StaffRepository.CountBySpecAsync(new StaffSpecification(request, true), cancellationToken);
        _logger.LogInformation("Fetched {Count} staffs", staffs.Count);
        return Result<PaginationResult<StaffDto>>.Ok(new PaginationResult<StaffDto>(
            staffs.Select(_mapper.Map<StaffDto>).ToArray(),
            totalCount,
            request.PageNumber,
            Math.Ceiling((decimal)totalCount / request.PageSize),
            request.PageSize));
    }

    public async Task<Result<StaffDto?>> CreateAsync(CreateStaffRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating staff {@Request}", request);
        var staff = _mapper.Map<Staff>(request);
        staff.StaffId = Guid.CreateVersion7();
        await _unitOfWork.StaffRepository.AddAsync(staff, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Staff created with ID {StaffId}", staff.StaffId);
        return Result<StaffDto?>.Created($"/api/staves/{staff.StaffId}", _mapper.Map<StaffDto>(staff));
    }

    public async Task<Result<StaffDto?>> UpdateAsync(Guid id, UpdateStaffRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating staff with ID {StaffId}", id);
        var staff = await _unitOfWork.StaffRepository.GetByIdAsync(id, cancellationToken);
        if (staff is null)
        {
            _logger.LogWarning("Staff with ID {StaffId} not found", id);
            return Result<StaffDto?>.NotFound(key: id, entityName: nameof(Staff));
        }

        staff.Name = request.Name;
        staff.Role = request.Role;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Staff with ID {StaffId} updated successfully", id);
        return Result<StaffDto?>.Ok(_mapper.Map<StaffDto>(staff));
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting staff with ID {StaffId}", id);
        var staff = await _unitOfWork.StaffRepository.GetByIdAsync(id, cancellationToken);
        if (staff is null)
        {
            _logger.LogWarning("Staff with ID {StaffId} not found", id);
            return Result<bool>.NotFound(key: id, entityName: nameof(Staff));
        }
        await _unitOfWork.StaffRepository.RemoveAsync(staff);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Staff with ID {StaffId} deleted successfully", id);
        return Result<bool>.NoContent();
    }
}
