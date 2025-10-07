using AutoMapper;
using OrderAndInventory.BLL.DTOs.StaffOrder.Requests;
using OrderAndInventory.BLL.DTOs.StaffOrder.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Mappers;

public class StaffOrderProfile : Profile
{
    public StaffOrderProfile()
    {
        CreateMap<CreateStaffOrderRequest, StaffOrder>();
        CreateMap<StaffOrder, StaffOrderDto>();
    }
}