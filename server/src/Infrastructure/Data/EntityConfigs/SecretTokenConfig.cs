using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class SecretTokenConfig : IEntityTypeConfiguration<SecretToken>
{
    public void Configure(EntityTypeBuilder<SecretToken> builder)
    {
        builder.HasKey(st => st.Id);
    }
}