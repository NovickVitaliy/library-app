using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database.EntityConfigurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.HasKey(x => x.StaffId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Role)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.StaffOrders)
            .WithOne(x => x.Staff)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}