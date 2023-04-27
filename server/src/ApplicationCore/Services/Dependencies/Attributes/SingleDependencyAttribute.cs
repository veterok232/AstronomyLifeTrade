using ApplicationCore.Services.Dependencies.Enums;

namespace ApplicationCore.Services.Dependencies.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class SingleDependencyAttribute : DependencyAttribute
{
    public SingleDependencyAttribute(params object[] key)
        : base(type: DependencyType.ImplementedInterfaces, lifetime: DependencyLifetime.Single, key: key)
    {
    }
}