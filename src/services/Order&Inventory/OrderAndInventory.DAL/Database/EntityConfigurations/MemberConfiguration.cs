using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database.EntityConfigurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(x => x.MemberId);

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Email)
            .IsRequired();
        
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(16);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Member)
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}