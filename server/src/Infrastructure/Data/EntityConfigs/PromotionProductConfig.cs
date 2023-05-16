using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class PromotionProductConfig : IEntityTypeConfiguration<PromotionProduct>
{
    public void Configure(EntityTypeBuilder<PromotionProduct> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.Promotion)
            .WithMany(p => p.Products)
            .HasForeignKey(c => c.PromotionId);
        
        builder.HasOne(c => c.Product)
            .WithMany()
            .HasForeignKey(c => c.ProductId);
    }
}