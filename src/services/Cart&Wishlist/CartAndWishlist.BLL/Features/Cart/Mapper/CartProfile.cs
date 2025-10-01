using AutoMapper;
using CartAndWishlist.BLL.Features.Cart.DTOs.Requests;
using CartAndWishlist.BLL.Features.Cart.DTOs.Responses;

namespace CartAndWishlist.BLL.Features.Cart.Mapper;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CreateCartRequest, Domain.Models.Cart>();

        CreateMap<Domain.Models.Cart, CartDto>();
    }
}