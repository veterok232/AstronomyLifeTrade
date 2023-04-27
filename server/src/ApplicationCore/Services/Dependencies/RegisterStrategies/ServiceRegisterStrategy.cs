using ApplicationCore.Extensions;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Services.Dependencies.Enums;
using ApplicationCore.Services.Dependencies.Extensions;
using Autofac;
using Autofac.Builder;

namespace ApplicationCore.Services.Dependencies.RegisterStrategies;

internal static class ServiceRegisterStrategy
{
    public static void Register(ContainerBuilder builder, Type type, DependencyAttribute dependency)
    {
        builder.RegisterType(type)
            .SpecifyLifetime(dependency.Lifetime)
            .SpecifyType(dependency)
            .SpecifyKey(dependency);
    }

    private static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> SpecifyLifetime(
        this IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder,
        DependencyLifetime? dependencyLifetime)
    {
        return dependencyLifetime switch
        {
            DependencyLifetime.Scoped => builder.InstancePerLifetimeScope(),
            DependencyLifetime.Single => builder.SingleInstance(),
            _ => throw new ArgumentOutOfRangeException(nameof(dependencyLifetime)),
        };
    }

    private static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> SpecifyType(
        this IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder,
        DependencyAttribute dependency)
    {
        return dependency switch
        {
            { } when !dependency.Key.IsNullOrEmpty() => builder,
            { Type: DependencyType.Self } => builder.AsSelf(),
            { Type: DependencyType.ImplementedInterfaces } => builder.AsImplementedInterfaces(),
            _ => throw new ArgumentOutOfRangeException(nameof(dependency)),
        };
    }

    private static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> SpecifyKey(
        this IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder,
        DependencyAttribute dependency)
    {
        return dependency switch
        {
            { } when dependency.Key.IsNullOrEmpty() => builder,
            { Type: DependencyType.Self } => builder.Keyed(dependency.Key, builder.ActivatorData.ImplementationType),
            { Type: DependencyType.ImplementedInterfaces } => builder.Keyed(
                dependency.Key, builder.ActivatorData.ImplementationType.GetInterfaces()),
            _ => throw new ArgumentOutOfRangeException(nameof(dependency)),
        };
    }
}