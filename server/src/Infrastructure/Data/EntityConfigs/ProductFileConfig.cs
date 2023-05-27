using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class ProductFileConfig : IEntityTypeConfiguration<ProductFile>
{
    public void Configure(EntityTypeBuilder<ProductFile> builder)
    {
        builder.HasKey(pf => pf.Id);

        builder.HasOne(pf => pf.File)
            .WithMany()
            .HasForeignKey(pf => pf.FileId);
        
        builder.HasOne(pf => pf.Product)
            .WithMany(p => p.Files)
            .HasForeignKey(pf => pf.ProductId);
    }
}