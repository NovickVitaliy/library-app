using AutoMapper;
using OrderAndInventory.BLL.DTOs.Payment.Requests;
using OrderAndInventory.BLL.DTOs.Payment.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Mappers;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<CreatePaymentRequest, Payment>();
        CreateMap<Payment, PaymentDto>();
    }
}