using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.DAL.Database.EntityConfigurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("chk_payments_amount_greater_than_zero", "amount > 0");

            t.HasCheckConstraint("chk_payments_paid_date_not_future", "paid_date <= Now()");
        });
        
        builder.HasKey(x => x.PaymentId);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.PaidDate)
            .IsRequired();

        builder.Property(x => x.PaymentMethod)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}