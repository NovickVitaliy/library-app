using AutoMapper;
using CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;
using CartAndWishlist.BLL.Features.WishlistItem.DTOs.Responses;

namespace CartAndWishlist.BLL.Features.WishlistItem.Mapper;

public class WishlistItemProfile : Profile
{
    public WishlistItemProfile()
    {
        CreateMap<CreateWishlistItemRequest, Domain.Models.WishlistItem>();
        CreateMap<Domain.Models.WishlistItem, WishlistItemDto>();
    }
}