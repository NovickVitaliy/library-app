using AutoMapper;
using OrderAndInventory.BLL.DTOs.Inventory.Requests;
using OrderAndInventory.BLL.DTOs.Inventory.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Mappers;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<CreateInventoryRequest, Inventory>();
        CreateMap<Inventory, InventoryDto>();
    }
}