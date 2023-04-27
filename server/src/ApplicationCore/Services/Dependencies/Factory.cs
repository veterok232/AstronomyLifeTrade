using ApplicationCore.Interfaces.Dependencies;
using ApplicationCore.Services.Dependencies.Attributes;
using Autofac;
using Autofac.Core;

namespace ApplicationCore.Services.Dependencies;

[ScopedDependency]
internal class Factory<TService> : IFactory<TService>
{
    private readonly IComponentContext _componentContext;

    public Factory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public TService CreateService(object key)
    {
        return (TService)_componentContext.ResolveService(new KeyedService(key, typeof(TService)));
    }
}