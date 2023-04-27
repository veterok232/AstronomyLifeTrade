namespace ApplicationCore.Services.Dependencies.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class DecoratorDependencyAttribute : DependencyAttribute
{
    public DecoratorDependencyAttribute()
        : base(isDecorator: true)
    {
    }
}