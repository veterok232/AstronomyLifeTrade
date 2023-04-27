using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Extensions;

internal static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigureOptimisticLock(this ModelBuilder modelBuilder)
    {
        modelBuilder.Model.GetEntityTypes()
            .Where(type => typeof(IHasVersion).IsAssignableFrom(type.ClrType))
            .Select(type => type.FindProperty(nameof(IHasVersion.Version)))
            .ForEach(MarkPropAsConcurrency);

        return modelBuilder;
    }

    private static void MarkPropAsConcurrency(IMutableProperty property)
    {
        property.IsConcurrencyToken = true;
        property.ValueGenerated = ValueGenerated.Never;
    }
}