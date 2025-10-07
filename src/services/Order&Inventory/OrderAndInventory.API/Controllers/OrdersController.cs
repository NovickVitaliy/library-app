using Microsoft.AspNetCore.Mvc;
using OrderAndInventory.BLL.DTOs.Order.Requests;
using OrderAndInventory.BLL.DTOs.Order.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using Shared.DTOs;

namespace OrderAndInventory.API.Controllers;

[Route("api/orders")]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        return (await _orderService.CreateAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _orderService.GetByIdAsync(id, cancellationToken)).ToApiResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetOrdersRequest request, CancellationToken cancellationToken)
    {
        return (await _orderService.GetAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _orderService.DeleteAsync(id, cancellationToken)).ToApiResponse();
    }
}