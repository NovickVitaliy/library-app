using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database;

public class OrderAndInventoryDbContext : DbContext
{
    public const string ConnectionStringConfigurationKey = "Default";

    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Staff> Staves => Set<Staff>();
    public DbSet<StaffOrder> StaffOrders => Set<StaffOrder>();
    
    public OrderAndInventoryDbContext(DbContextOptions<OrderAndInventoryDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        DatabaseSeeder.SeedDatabase(modelBuilder);
        
        // this call to base method is not necessary since it is empty
        base.OnModelCreating(modelBuilder);
    }
}