using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.Order)
            .WithMany()
            .HasForeignKey(c => c.OrderId);
        
        builder.HasOne(c => c.ConsumerAssignment)
            .WithMany()
            .HasForeignKey(c => c.ConsumerAssignmentId);
    }
}