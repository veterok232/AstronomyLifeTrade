using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigs;

public class ObjectForObservationConfig : IEntityTypeConfiguration<ObjectForObservation>
{
    public void Configure(EntityTypeBuilder<ObjectForObservation> builder)
    {
        builder.HasKey(sp => sp.Id);
    }
}