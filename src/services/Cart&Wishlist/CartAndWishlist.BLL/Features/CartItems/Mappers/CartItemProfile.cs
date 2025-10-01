using AutoMapper;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;
using CartAndWishlist.BLL.Features.CartItems.DTOs.Responses;
using CartAndWishlist.Domain.Models;

namespace CartAndWishlist.BLL.Features.CartItems.Mappers;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CreateCartItemRequest, CartItem>();
        CreateMap<CartItem, CartItemDto>();
    }
}