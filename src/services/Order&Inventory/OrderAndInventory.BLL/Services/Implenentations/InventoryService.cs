using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OrderAndInventory.BLL.DTOs.Inventory.Requests;
using OrderAndInventory.BLL.DTOs.Inventory.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using OrderAndInventory.BLL.Specifications;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.UOW.Interfaces;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Implenentations;

public class InventoryService : IInventoryService
{
    private readonly ILogger<InventoryService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateInventoryRequest> _createInventoryRequestValidator;
    
    public InventoryService(
        ILogger<InventoryService> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateInventoryRequest> createInventoryRequestValidator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createInventoryRequestValidator = createInventoryRequestValidator;
    }
    
    public async Task<Result<InventoryDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching inventory with ID: {InventoryId}", id);

        var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(id, cancellationToken);

        if (inventory is null)
        {
            _logger.LogWarning("Inventory with ID {InventoryId} not found", id);
            return Result<InventoryDto?>.NotFound(key: id, entityName: nameof(Inventory));
        }

        _logger.LogInformation("Successfully retrieved inventory with ID: {InventoryId}", id);
        return Result<InventoryDto?>.Ok(_mapper.Map<InventoryDto>(inventory));
    }
    
    public async Task<Result<PaginationResult<InventoryDto>>> GetAsync(GetInventoriesRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching inventories with filters: {@Request}", request);

        var specification = new InventorySpecification(request);
        var inventories = (await _unitOfWork.InventoryRepository.ListBySpecAsync(specification, cancellationToken)).ToList();
        var totalCount = await _unitOfWork.InventoryRepository.CountBySpecAsync(new InventorySpecification(request, true), cancellationToken);

        _logger.LogInformation("Fetched {Count} inventories (Page {PageNumber}, PageSize {PageSize})",
            inventories.Count, request.PageNumber, request.PageSize);

        return Result<PaginationResult<InventoryDto>>.Ok(new PaginationResult<InventoryDto>(
            inventories.Select(_mapper.Map<InventoryDto>).ToArray(),
            totalCount,
            request.PageNumber,
            Math.Ceiling((decimal)totalCount / request.PageSize),
            request.PageSize));
    }
    
    public async Task<Result<InventoryDto?>> CreateAsync(CreateInventoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new inventory entry: {@Request}", request);

        var validationResult = await _createInventoryRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for creating inventory: {Error}",
                validationResult.Errors[0].ErrorMessage);
            
            return Result<InventoryDto?>.BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        var inventory = _mapper.Map<Inventory>(request);
        inventory.InventoryId = Guid.CreateVersion7();

        await _unitOfWork.InventoryRepository.AddAsync(inventory, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Inventory created successfully with ID: {InventoryId}", inventory.InventoryId);

        return Result<InventoryDto?>.Ok(_mapper.Map<InventoryDto>(inventory));
    }
    
    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete inventory with ID: {InventoryId}", id);

        var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(id, cancellationToken);
        if (inventory is null)
        {
            _logger.LogWarning("Cannot delete inventory. ID {InventoryId} not found", id);
            return Result<bool>.NotFound(key: id, entityName: nameof(Inventory));
        }

        await _unitOfWork.InventoryRepository.RemoveAsync(inventory);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Inventory with ID {InventoryId} deleted successfully", id);

        return Result<bool>.NoContent();
    }
    
    public async Task<Result<InventoryDto?>> UpdateAsync(Guid id, UpdateInventoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating inventory with ID: {InventoryId} using {@Request}", id, request);

        var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(id, cancellationToken);
        if (inventory is null)
        {
            _logger.LogWarning("Cannot update inventory. ID {InventoryId} not found", id);
            return Result<InventoryDto?>.NotFound(key: id, entityName: nameof(Inventory));
        }

        inventory.ReorderLevel = request.ReorderLevel;
        inventory.StockQuantity = request.StockQuantity;

        await _unitOfWork.InventoryRepository.UpdateAsync(inventory);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Inventory with ID {InventoryId} updated successfully", id);

        return Result<InventoryDto?>.Ok(_mapper.Map<InventoryDto>(inventory));
    }
}
