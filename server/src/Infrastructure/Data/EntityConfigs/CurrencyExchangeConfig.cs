using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class CurrencyExchangeConfig : IEntityTypeConfiguration<CurrencyExchange>
{
    public void Configure(EntityTypeBuilder<CurrencyExchange> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.FromCurrency)
            .WithMany()
            .HasForeignKey(c => c.FromCurrencyId);
        
        builder.HasOne(c => c.ToCurrency)
            .WithMany()
            .HasForeignKey(c => c.ToCurrencyId);
    }
}