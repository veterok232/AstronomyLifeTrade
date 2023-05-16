using ApplicationCore.Entities;
using Infrastructure.Constants;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class OrderItemProductItemConfig : IEntityTypeConfiguration<OrderItemProductItem>
{
    public void Configure(EntityTypeBuilder<OrderItemProductItem> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.OrderItem)
            .WithMany(oi => oi.ProductItems)
            .HasForeignKey(c => c.OrderItemId);
        
        builder.HasOne(c => c.ProductItem)
            .WithMany()
            .HasForeignKey(c => c.ProductItemId);
    }
}