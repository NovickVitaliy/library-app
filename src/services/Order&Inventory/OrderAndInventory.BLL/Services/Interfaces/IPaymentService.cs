using OrderAndInventory.BLL.DTOs.Payment.Requests;
using OrderAndInventory.BLL.DTOs.Payment.Responses;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Interfaces;

public interface IPaymentService
{
    Task<Result<PaymentDto?>> CreatePaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken);
    Task<Result<PaginationResult<PaymentDto>>> GetAsync(GetPaymentsRequest request, CancellationToken cancellationToken);
    Task<Result<PaymentDto?>> GetPaymentAsync(Guid id, CancellationToken cancellationToken);
}