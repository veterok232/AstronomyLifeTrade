using ApplicationCore.Services.Dependencies.Attributes;
using Autofac;

namespace ApplicationCore.Services.Dependencies.RegisterStrategies;

internal static class DecoratorServiceRegisterStrategy
{
    public static void Register(ContainerBuilder builder, Type type, DependencyAttribute dependency)
    {
        foreach (var serviceType in type.GetInterfaces())
        {
            builder.RegisterDecorator(type, serviceType);
        }
    }
}