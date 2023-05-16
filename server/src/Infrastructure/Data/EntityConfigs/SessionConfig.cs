using ApplicationCore.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class SessionConfig : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId);

        builder.HasOne(s => s.Assignment)
            .WithMany()
            .HasForeignKey(s => s.AssignmentId);

        builder.Property(s => s.Fingerprint).IsRequired().HasMaxLength(DbConfigConstants.DefaultTextColumnLength);
    }
}