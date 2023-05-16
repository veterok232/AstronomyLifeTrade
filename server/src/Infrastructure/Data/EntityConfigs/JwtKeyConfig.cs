using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class JwtKeyConfig : IEntityTypeConfiguration<JwtKey>
{
    public void Configure(EntityTypeBuilder<JwtKey> builder)
    {
        builder.HasKey(jk => jk.Id);

        builder.HasOne(jk => jk.SecretToken)
            .WithOne()
            .HasForeignKey<JwtKey>(jk => jk.SecretId);
    }
}