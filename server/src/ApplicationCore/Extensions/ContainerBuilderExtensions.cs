using Autofac;

namespace ApplicationCore.Extensions;

public static class ContainerBuilderExtensions
{
    public static ContainerBuilder AddScopedAsImplementedInterfaces<TService>(this ContainerBuilder builder)
    {
        builder.RegisterType<TService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static ContainerBuilder AddSingleAsImplementedInterfaces<TService>(this ContainerBuilder builder)
    {
        builder.RegisterType<TService>()
            .AsImplementedInterfaces()
            .SingleInstance();

        return builder;
    }

    public static ContainerBuilder AddScopedAsSelf<TService>(this ContainerBuilder builder)
    {
        builder.RegisterType<TService>()
            .AsSelf()
            .InstancePerLifetimeScope();

        return builder;
    }
}