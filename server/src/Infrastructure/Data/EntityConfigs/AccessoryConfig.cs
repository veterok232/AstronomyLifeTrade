using ApplicationCore.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

internal class AccessoryConfig : IEntityTypeConfiguration<Accessory>
{
    public void Configure(EntityTypeBuilder<Accessory> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.Product)
            .WithMany()
            .HasForeignKey(u => u.ProductId);
    }
}