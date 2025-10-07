using AutoMapper;
using OrderAndInventory.BLL.DTOs.Order.Requests;
using OrderAndInventory.BLL.DTOs.Order.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, Order>();
        CreateMap<Order, OrderDto>();
    }
}