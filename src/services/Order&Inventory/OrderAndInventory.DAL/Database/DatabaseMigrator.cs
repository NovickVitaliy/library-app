using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrderAndInventory.DAL.Database;

public static class DatabaseMigrator
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        await using var dbContext = scope.ServiceProvider.GetRequiredService<OrderAndInventoryDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}