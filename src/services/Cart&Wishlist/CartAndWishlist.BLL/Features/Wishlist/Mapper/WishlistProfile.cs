using AutoMapper;
using CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;
using CartAndWishlist.BLL.Features.Wishlist.DTOs.Responses;

namespace CartAndWishlist.BLL.Features.Wishlist.Mapper;

public class WishlistProfile : Profile
{
    public WishlistProfile()
    {
        CreateMap<CreateWishlistRequest, Domain.Models.Wishlist>();

        CreateMap<Domain.Models.Wishlist, WishlistDto>();
    }
}