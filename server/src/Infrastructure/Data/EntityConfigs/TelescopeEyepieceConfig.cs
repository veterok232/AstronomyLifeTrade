using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class TelescopeEyepieceConfig : IEntityTypeConfiguration<TelescopeEyepiece>
{
    public void Configure(EntityTypeBuilder<TelescopeEyepiece> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Telescope)
            .WithMany(t => t.TelescopeEyepieces)
            .HasForeignKey(s => s.TelescopeId);
    }
}