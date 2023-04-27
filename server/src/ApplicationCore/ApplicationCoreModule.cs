using ApplicationCore.Services.Dependencies.Extensions;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace ApplicationCore;

public class ApplicationCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMediatR(ThisAssembly);

        builder.RegisterDependenciesInAssembly(ThisAssembly);

        base.Load(builder);
    }
}