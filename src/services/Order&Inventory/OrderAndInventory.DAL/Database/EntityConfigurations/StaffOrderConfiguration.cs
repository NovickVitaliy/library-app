using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database.EntityConfigurations;

public class StaffOrderConfiguration : IEntityTypeConfiguration<StaffOrder>
{
    public void Configure(EntityTypeBuilder<StaffOrder> builder)
    {
        builder.HasKey(x => new { x.OrderId, x.StaffId });

        builder.HasOne(x => x.Order)
            .WithMany(x => x.StaffOrders)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Staff)
            .WithMany(x => x.StaffOrders)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}