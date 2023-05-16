using ApplicationCore.Entities;
using Infrastructure.Constants;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(c => c.OrderId);

        builder.HasOne(c => c.Product)
            .WithMany()
            .HasForeignKey(c => c.ProductId);
    }
}