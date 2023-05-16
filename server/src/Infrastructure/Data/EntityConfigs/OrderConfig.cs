using ApplicationCore.Entities;
using Infrastructure.Constants;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.ConsumerAssignment)
            .WithMany()
            .HasForeignKey(c => c.ConsumerAssignmentId);
        
        builder.HasOne(c => c.ManagerAssignment)
            .WithMany()
            .HasForeignKey(c => c.ManagerAssignmentId);
        
        builder.Property(p => p.OrderNumber).HasDefaultValueSql(
                DbSequenceUtils.CreateSequenceFormula(
                    DbConfigConstants.Sequences.OrderNumberSequence))
            .IsRequired();
    }
}