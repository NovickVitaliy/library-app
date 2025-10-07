using Microsoft.AspNetCore.Mvc;
using OrderAndInventory.BLL.DTOs.Payment.Requests;
using OrderAndInventory.BLL.DTOs.Payment.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using Shared.DTOs;

namespace OrderAndInventory.API.Controllers;

[Route("api/payments")]
public class PaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;
    
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostAsync(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        return (await _paymentService.CreatePaymentAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<PaymentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetPaymentsRequest request, CancellationToken cancellationToken)
    {
        return (await _paymentService.GetAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _paymentService.GetPaymentAsync(id, cancellationToken)).ToApiResponse();
    }
}