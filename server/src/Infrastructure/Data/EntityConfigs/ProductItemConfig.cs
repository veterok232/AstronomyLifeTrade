using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class ProductItemConfig : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.HasKey(sp => sp.Id);
        builder.HasIndex(sp => sp.SerialNumber);
        builder.HasIndex(sp => sp.Sku);

        builder.HasOne(c => c.Product)
            .WithMany(p => p.ProductItems)
            .HasForeignKey(c => c.ProductId);
    }
}