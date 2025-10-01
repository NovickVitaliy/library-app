using CartAndWishlist.DAL.Database;
using CartAndWishlist.DAL.Database.ConnectionAccessor;
using CartAndWishlist.DAL.Repositories;
using CartAndWishlist.DAL.Repositories.Implementations;
using CartAndWishlist.DAL.Repositories.Interfaces;
using CartAndWishlist.DAL.Repositories.UOW.Implementations;
using CartAndWishlist.DAL.Repositories.UOW.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Exceptions;

namespace CartAndWishlist.DAL;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        services.AddScoped<IDatabaseConnectionAccessor, NpgsqlConnectionAccessor>(_ => new NpgsqlConnectionAccessor(
            configuration.GetConnectionString(IDatabaseConnectionAccessor.DatabaseConnectionConfigurationKey)
            ?? throw new ItemInConfigurationNotFoundException(IDatabaseConnectionAccessor.DatabaseConnectionConfigurationKey)));

        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();

        services.AddScoped<RepositoryBase, CartItemRepository>();
        services.AddScoped<RepositoryBase, CartRepository>();
        services.AddScoped<RepositoryBase, WishlistRepository>();
        services.AddScoped<RepositoryBase, WishlistItemRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}