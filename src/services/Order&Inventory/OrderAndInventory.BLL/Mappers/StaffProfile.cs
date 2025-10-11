using AutoMapper;
using OrderAndInventory.BLL.DTOs.Staff.Requests;
using OrderAndInventory.BLL.DTOs.Staff.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Mappers;

public class StaffProfile : Profile
{
    public StaffProfile()
    {
        CreateMap<CreateStaffRequest, Staff>();
        CreateMap<Staff, StaffDto>()
            .ForCtorParam("Orders", opt => opt.MapFrom(x => x.StaffOrders.Select(y => y.Order)));
    }
}