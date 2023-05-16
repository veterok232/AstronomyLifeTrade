using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class LegalDetailsConfig : IEntityTypeConfiguration<LegalDetails>
{
    public void Configure(EntityTypeBuilder<LegalDetails> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.LegalAddress)
            .WithMany()
            .HasForeignKey(c => c.LegalAddressId);
    }
}