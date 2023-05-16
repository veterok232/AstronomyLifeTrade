using ApplicationCore.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class BinocularConfig : IEntityTypeConfiguration<Binocular>
{
    public void Configure(EntityTypeBuilder<Binocular> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Product)
            .WithMany()
            .HasForeignKey(a => a.ProductId);
    }
}