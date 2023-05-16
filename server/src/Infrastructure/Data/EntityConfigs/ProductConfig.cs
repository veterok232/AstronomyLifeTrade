using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(sp => sp.Id);
        builder.HasIndex(sp => sp.Code);

        builder.HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);
        
        builder.HasOne(c => c.Category)
            .WithMany()
            .HasForeignKey(c => c.CategoryId);
    }
}