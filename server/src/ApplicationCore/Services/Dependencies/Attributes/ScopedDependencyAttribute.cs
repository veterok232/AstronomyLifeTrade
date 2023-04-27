using ApplicationCore.Services.Dependencies.Enums;

namespace ApplicationCore.Services.Dependencies.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ScopedDependencyAttribute : DependencyAttribute
{
    public ScopedDependencyAttribute(params object[] key)
        : base(type: DependencyType.ImplementedInterfaces, lifetime: DependencyLifetime.Scoped, key: key)
    {
    }
}