using ApplicationCore.Services.Dependencies.Enums;

namespace ApplicationCore.Services.Dependencies.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DependencyAttribute : Attribute
{
    public DependencyAttribute(
        DependencyType type,
        DependencyLifetime lifetime,
        params object[] key)
    {
        Type = type;
        Lifetime = lifetime;
        Key = key.Distinct();
    }

    public DependencyAttribute(bool isDecorator)
    {
        IsDecorator = isDecorator;
    }

    public DependencyAttribute(
        Type factoryForType,
        DependencyLifetime lifetime)
    {
        FactoryForType = factoryForType;
        Lifetime = lifetime;
    }

    public DependencyType? Type { get; }

    public DependencyLifetime? Lifetime { get; }

    public IEnumerable<object> Key { get; }

    public bool IsDecorator { get; }

    public Type FactoryForType { get; }
}