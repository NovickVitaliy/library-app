using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database.EntityConfigurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("chk_inventories_stock_quantity_greater_than_zero", "stock_quantity > 0");

            t.HasCheckConstraint("chk_inventories_reorder_level_greater_than_zero", "reorder_level > 0");
        });
        
        builder.HasKey(x => x.InventoryId);

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.StockQuantity)
            .IsRequired();

        builder.Property(x => x.ReorderLevel)
            .IsRequired();
    }
}