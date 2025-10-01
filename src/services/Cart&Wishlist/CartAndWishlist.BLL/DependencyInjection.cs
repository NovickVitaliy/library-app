using CartAndWishlist.BLL.Features.Cart.Services.Implementation;
using CartAndWishlist.BLL.Features.Cart.Services.Interfaces;
using CartAndWishlist.BLL.Features.CartItems.Services.Implementations;
using CartAndWishlist.BLL.Features.CartItems.Services.Interfaces;
using CartAndWishlist.BLL.Features.Wishlist.Services.Implementations;
using CartAndWishlist.BLL.Features.Wishlist.Services.Interfaces;
using CartAndWishlist.BLL.Features.WishlistItem.Services.Implementations;
using CartAndWishlist.BLL.Features.WishlistItem.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CartAndWishlist.BLL;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureBusinessLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => {},
            typeof(DependencyInjection));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<ICartItemsService, CartItemsService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<IWishlistItemService, WishlistItemService>();
        
        return services;
    }
}