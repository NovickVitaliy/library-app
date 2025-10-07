using Microsoft.AspNetCore.Mvc;
using OrderAndInventory.BLL.DTOs.Staff.Requests;
using OrderAndInventory.BLL.DTOs.Staff.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using Shared.DTOs;

namespace OrderAndInventory.API.Controllers;

[Route("api/staves")]
public class StavesController : BaseApiController
{
    private readonly IStaffService _staffService;
    
    public StavesController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(StaffDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateStaffRequest request, CancellationToken cancellationToken)
    {
        return (await _staffService.CreateAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(StaffDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _staffService.GetByIdAsync(id, cancellationToken)).ToApiResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<StaffDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetStavesRequest request, CancellationToken cancellationToken)
    {
        return (await _staffService.GetAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(StaffDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateStaffRequest request, CancellationToken cancellationToken)
    {
        return (await _staffService.UpdateAsync(id, request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _staffService.DeleteAsync(id, cancellationToken)).ToApiResponse();
    }
}