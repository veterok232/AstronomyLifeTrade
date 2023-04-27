using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Services.Dependencies.Enums;
using Autofac;
using Autofac.Builder;

namespace ApplicationCore.Services.Dependencies.RegisterStrategies;

internal static class GenericServiceRegisterStrategy
{
    public static void Register(ContainerBuilder builder, Type type, DependencyAttribute dependency)
    {
        builder.RegisterGeneric(type)
            .SpecifyLifetime(dependency.Lifetime)
            .SpecifyType(dependency.Type);
    }

    private static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> SpecifyLifetime(
        this IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> builder,
        DependencyLifetime? dependencyLifetime)
    {
        return dependencyLifetime switch
        {
            DependencyLifetime.Scoped => builder.InstancePerLifetimeScope(),
            DependencyLifetime.Single => builder.SingleInstance(),
            _ => throw new ArgumentOutOfRangeException(nameof(dependencyLifetime)),
        };
    }

    private static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> SpecifyType(
        this IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> builder,
        DependencyType? type)
    {
        return type switch
        {
            DependencyType.Self => builder.AsSelf(),
            DependencyType.ImplementedInterfaces => builder.AsImplementedInterfaces(),
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }
}