using System.Reflection;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Services.Dependencies.RegisterStrategies;
using Autofac;

namespace ApplicationCore.Services.Dependencies.Extensions;

public static class ContainerBuilderExtensions
{
    public static void RegisterDependenciesInAssembly(this ContainerBuilder builder, Assembly assembly)
    {
        foreach (var type in assembly.GetTypesWithAttribute<DependencyAttribute>())
        {
            var dependency = type.GetCustomAttribute<DependencyAttribute>();
            GetRegistrationStrategy(type, dependency)
                .Invoke(builder, type, dependency);
        }
    }

    private static Action<ContainerBuilder, Type, DependencyAttribute> GetRegistrationStrategy(
        Type type, DependencyAttribute dependency)
    {
        return dependency switch
        {
            { IsDecorator: true } => DecoratorServiceRegisterStrategy.Register,
            { FactoryForType: { } } => DependencyFactoryServiceRegisterStrategy.Register,
            { } when type.IsGenericType => GenericServiceRegisterStrategy.Register,
            _ => ServiceRegisterStrategy.Register,
        };
    }
}