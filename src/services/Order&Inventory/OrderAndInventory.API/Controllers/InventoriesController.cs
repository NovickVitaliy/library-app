using Microsoft.AspNetCore.Mvc;
using OrderAndInventory.BLL.DTOs.Inventory.Requests;
using OrderAndInventory.BLL.DTOs.Inventory.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using Shared.DTOs;

namespace OrderAndInventory.API.Controllers;

[Route("api/inventories")]
public class InventoriesController : BaseApiController
{
    private readonly IInventoryService _inventoryService;
    
    public InventoriesController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(InventoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateInventoryRequest request, CancellationToken cancellationToken)
    {
        return (await _inventoryService.CreateAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InventoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _inventoryService.GetByIdAsync(id, cancellationToken)).ToApiResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<InventoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetInventoriesRequest request, CancellationToken cancellationToken)
    {
        return (await _inventoryService.GetAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(InventoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateInventoryRequest request, CancellationToken cancellationToken)
    {
        return (await _inventoryService.UpdateAsync(id, request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _inventoryService.DeleteAsync(id, cancellationToken)).ToApiResponse();
    }
}