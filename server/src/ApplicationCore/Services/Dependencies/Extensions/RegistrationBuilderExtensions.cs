using Autofac.Builder;

namespace ApplicationCore.Services.Dependencies.Extensions;

internal static class RegistrationBuilderExtensions
{
    public static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> Keyed(
        this IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder,
        IEnumerable<object> keys,
        IEnumerable<Type> serviceTypes)
    {
        return serviceTypes.Aggregate(builder, (processingBuilder, type) => keys.Aggregate(processingBuilder, (b, key) => b.Keyed(key, type)));
    }
}