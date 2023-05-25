using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.FirstName);
        builder.HasIndex(u => u.LastName);
        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(DbConfigConstants.DefaultTextColumnLength)
            .HasColumnType(PostgreSqlDataTypes.Citext);
        builder.Property(u => u.PasswordHash).HasMaxLength(DbConfigConstants.DefaultTextColumnLength);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(DbConfigConstants.DefaultTextColumnLength);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(DbConfigConstants.DefaultTextColumnLength);
    }
}