using ApplicationCore.Services.Dependencies.Enums;

namespace ApplicationCore.Services.Dependencies.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class SelfScopedDependencyAttribute : DependencyAttribute
{
    public SelfScopedDependencyAttribute(params object[] key)
        : base(type: DependencyType.Self, lifetime: DependencyLifetime.Scoped, key: key)
    {
    }
}