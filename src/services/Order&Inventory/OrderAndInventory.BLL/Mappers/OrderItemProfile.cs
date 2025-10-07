using AutoMapper;
using OrderAndInventory.BLL.DTOs.OrderItem.Requests;
using OrderAndInventory.BLL.DTOs.OrderItem.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Mappers;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<CreateOrderItemRequest, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}