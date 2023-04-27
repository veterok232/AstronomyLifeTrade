using ApplicationCore.Services.Dependencies.Extensions;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class InfrastructureModule : Module
{
    private readonly IConfiguration _configuration;

    public InfrastructureModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
        // It was then registered with Autofac using the Populate method. All of this starts
        // with the `UseServiceProviderFactory(new AutofacServiceProviderFactory())` that happens in Program and registers Autofac
        // as the service provider.
        builder.RegisterMediatR(ThisAssembly);

        builder.RegisterDependenciesInAssembly(ThisAssembly);
    }
}