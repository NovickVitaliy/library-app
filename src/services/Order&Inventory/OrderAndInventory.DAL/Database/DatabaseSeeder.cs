using Microsoft.EntityFrameworkCore;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database;

public static class DatabaseSeeder
{
    public static void SeedDatabase(ModelBuilder modelBuilder)
    {
        SeedInventories(modelBuilder);
        SeedMembers(modelBuilder);
    }

    private static void SeedMembers(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Member>()
            .HasData(new Member
                {
                    MemberId = new Guid("11111111-1111-1111-1111-111111111111"),
                    Email = "john.doe@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "(555) 123-4567"
                },
                new Member
                {
                    MemberId = new Guid("22222222-2222-2222-2222-222222222222"),
                    Email = "jane.smith@example.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "(555) 234-5678"
                },
                new Member
                {
                    MemberId = new Guid("33333333-3333-3333-3333-333333333333"),
                    Email = "alice.johnson@example.com",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    PhoneNumber = "(555) 345-6789"
                },
                new Member
                {
                    MemberId = new Guid("44444444-4444-4444-4444-444444444444"),
                    Email = "bob.williams@example.com",
                    FirstName = "Bob",
                    LastName = "Williams",
                    PhoneNumber = "(555) 456-7890"
                },
                new Member
                {
                    MemberId = new Guid("55555555-5555-5555-5555-555555555555"),
                    Email = "emma.brown@example.com",
                    FirstName = "Emma",
                    LastName = "Brown",
                    PhoneNumber = "(555) 567-8901"
                });
    }

    private static void SeedInventories(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Inventory>()
            .HasData(
                new Inventory
                {
                    InventoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BookId = new Guid("21111111-1111-1111-1111-111111111111"),
                    StockQuantity = 100,
                    ReorderLevel = 20
                },
                new Inventory
                {
                    InventoryId = new Guid("22222222-2222-2222-2222-222222222222"),
                    BookId = new Guid("32222222-2222-2222-2222-222222222222"),
                    StockQuantity = 50,
                    ReorderLevel = 15
                },
                new Inventory
                {
                    InventoryId = new Guid("33333333-3333-3333-3333-333333333333"),
                    BookId = new Guid("43333333-3333-3333-3333-333333333333"),
                    StockQuantity = 75,
                    ReorderLevel = 25
                },
                new Inventory
                {
                    InventoryId = new Guid("44444444-4444-4444-4444-444444444444"),
                    BookId = new Guid("54444444-4444-4444-4444-444444444444"),
                    StockQuantity = 30,
                    ReorderLevel = 10
                },
                new Inventory
                {
                    InventoryId = new Guid("55555555-5555-5555-5555-555555555555"),
                    BookId = new Guid("65555555-5555-5555-5555-555555555555"),
                    StockQuantity = 200,
                    ReorderLevel = 50
                }
            );
    }
}