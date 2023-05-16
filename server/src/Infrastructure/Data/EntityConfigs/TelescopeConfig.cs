using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class TelescopeConfig : IEntityTypeConfiguration<Telescope>
{
    public void Configure(EntityTypeBuilder<Telescope> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId);
    }
}