using ApplicationCore.Interfaces.Dependencies;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Services.Dependencies.Enums;
using ApplicationCore.Services.Dependencies.Extensions;
using Autofac;
using Autofac.Builder;

namespace ApplicationCore.Services.Dependencies.RegisterStrategies;

internal static class DependencyFactoryServiceRegisterStrategy
{
    public static void Register(ContainerBuilder builder, Type type, DependencyAttribute dependency)
    {
        ServiceRegisterStrategy.Register(builder, type, new SelfScopedDependencyAttribute());
        builder.Register(context => context.Resolve(type).As<IDependencyFactory>().Create())
            .As(dependency.FactoryForType)
            .SpecifyLifetime(dependency.Lifetime);
    }

    private static IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle> SpecifyLifetime(
        this IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle> builder,
        DependencyLifetime? dependencyLifetime)
    {
        return dependencyLifetime switch
        {
            DependencyLifetime.Scoped => builder.InstancePerLifetimeScope(),
            DependencyLifetime.Single => builder.SingleInstance(),
            _ => throw new ArgumentOutOfRangeException(nameof(dependencyLifetime)),
        };
    }
}