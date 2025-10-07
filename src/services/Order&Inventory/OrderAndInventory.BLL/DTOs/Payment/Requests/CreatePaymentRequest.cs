using System.ComponentModel.DataAnnotations;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.DTOs.Payment.Requests;

public record CreatePaymentRequest(
        [Required(ErrorMessage = "Order Id is required")] Guid OrderId,
        [Required(ErrorMessage = "Amount is required")] decimal Amount,
        [Required(ErrorMessage = "Payment method")] PaymentMethod PaymentMethod);