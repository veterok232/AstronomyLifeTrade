using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class PersonalDataConfig : IEntityTypeConfiguration<PersonalData>
{
    public void Configure(EntityTypeBuilder<PersonalData> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.HasOne(c => c.Address)
            .WithMany()
            .HasForeignKey(c => c.AddressId);
        
        builder.HasOne(c => c.Assignment)
            .WithMany()
            .HasForeignKey(c => c.AssignmentId);
        
        builder.HasOne(c => c.LegalDetails)
            .WithMany()
            .HasForeignKey(c => c.LegalDetailsId);
    }
}