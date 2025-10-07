using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderAndInventory.BLL.Services.Implenentations;
using OrderAndInventory.BLL.Services.Interfaces;

namespace OrderAndInventory.BLL;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureBusinessLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => {}, Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IStaffService, StaffService>();
        
        return services;
    }
}