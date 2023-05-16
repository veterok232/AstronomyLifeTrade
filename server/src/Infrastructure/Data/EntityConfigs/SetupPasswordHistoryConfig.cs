using ApplicationCore.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class SetupPasswordHistoryConfig : IEntityTypeConfiguration<SetupPasswordHistory>
{
    public void Configure(EntityTypeBuilder<SetupPasswordHistory> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.PasswordHash).IsRequired().HasMaxLength(DbConfigConstants.DefaultTextColumnLength);
        builder.HasOne<User>().WithMany().HasForeignKey(t => t.UserId);
    }
}