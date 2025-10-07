using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderAndInventory.DAL.Database;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.Repositories.Implementations;
using OrderAndInventory.DAL.Repositories.Interfaces;
using OrderAndInventory.DAL.UOW.Implementations;
using OrderAndInventory.DAL.UOW.Interfaces;
using Shared.Exceptions;

namespace OrderAndInventory.DAL;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderAndInventoryDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString(
                              OrderAndInventoryDbContext.ConnectionStringConfigurationKey)
                          ?? throw new ItemInConfigurationNotFoundException(OrderAndInventoryDbContext.ConnectionStringConfigurationKey));

            opt.UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture);
            
            var staff1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var staff2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var staff3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var staff4Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var staff5Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            
            opt.UseSeeding((context, _) =>
            {
                if (!context.Set<Staff>().Any())
                {
                    var staffList = new[]
                    {
                        new Staff { StaffId = staff1Id, Name = "John Doe", Role = "Manager" },
                        new Staff { StaffId = staff2Id, Name = "Alice Smith", Role = "Cashier" },
                        new Staff { StaffId = staff3Id, Name = "Bob Johnson", Role = "Warehouse" },
                        new Staff { StaffId = staff4Id, Name = "Eve Williams", Role = "Supervisor" },
                        new Staff { StaffId = staff5Id, Name = "Charlie Brown", Role = "Technician" }
                    };

                    context.Set<Staff>().AddRange(staffList);
                    context.SaveChanges();
                }
            });
            
            opt.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                if (!await context.Set<Staff>().AnyAsync(cancellationToken))
                {
                    var staffList = new[]
                    {
                        new Staff { StaffId = staff1Id, Name = "John Doe", Role = "Manager" },
                        new Staff { StaffId = staff2Id, Name = "Alice Smith", Role = "Cashier" },
                        new Staff { StaffId = staff3Id, Name = "Bob Johnson", Role = "Warehouse" },
                        new Staff { StaffId = staff4Id, Name = "Eve Williams", Role = "Supervisor" },
                        new Staff { StaffId = staff5Id, Name = "Charlie Brown", Role = "Technician" }
                    };

                    await context.Set<Staff>().AddRangeAsync(staffList, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }
            });
        });

        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}