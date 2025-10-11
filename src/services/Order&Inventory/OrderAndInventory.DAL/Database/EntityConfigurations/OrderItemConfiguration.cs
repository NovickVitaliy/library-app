using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database.EntityConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(x =>
        {
            x.HasCheckConstraint("chk_order_items_quantity_greater_than_zero", "quantity > 0");

            x.HasCheckConstraint("chk_order_items_unit_price_greater_than_zero", "unit_price > 0");
        });

        builder.HasKey(x => x.OrderItemId);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .IsRequired();

        builder.Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();
    }
}