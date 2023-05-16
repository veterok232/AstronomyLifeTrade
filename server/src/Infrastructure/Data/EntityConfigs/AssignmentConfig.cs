using ApplicationCore.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class AssignmentConfig : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .IsRequired();

        builder.Property(a => a.AffiliateNumber)
            .IsRequired(false)
            .HasMaxLength(DbConfigConstants.DefaultNameTextColumnLength);

        builder.HasOne(a => a.Role)
            .WithMany()
            .HasForeignKey(a => a.RoleId)
            .IsRequired();

        builder.HasOne(a => a.CreatedByUser)
            .WithMany()
            .HasForeignKey(a => a.CreatedByUserId);

        builder.Property(p => p.Phone)
            .HasMaxLength(DbConfigConstants.DefaultTextColumnLength)
            .IsRequired();
    }
}