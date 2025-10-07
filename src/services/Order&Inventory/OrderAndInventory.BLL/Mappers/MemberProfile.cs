using AutoMapper;
using OrderAndInventory.BLL.DTOs.Member.Requests;
using OrderAndInventory.BLL.DTOs.Member.Responses;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Mappers;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<CreateMemberRequest, Member>();
        CreateMap<Member, MemberDto>();
    }
}