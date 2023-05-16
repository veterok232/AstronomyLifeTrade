using ApplicationCore.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class SetupPasswordTokenConfig : IEntityTypeConfiguration<SetupPasswordToken>
{
    public void Configure(EntityTypeBuilder<SetupPasswordToken> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.TokenValue).IsRequired().HasMaxLength(DbConfigConstants.DefaultTextColumnLength);
        builder.HasOne<User>().WithMany().HasForeignKey(t => t.UserId);
    }
}