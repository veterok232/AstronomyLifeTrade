using System.IdentityModel.Tokens.Jwt;
using ApplicationCore.Extensions;
using ApplicationCore.Services.Dependencies.Extensions;
using Autofac;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Services.Jwt.SigningKeys;
using Infrastructure.Services.Storage.Files;
using Infrastructure.Settings;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
        
        AddFileStorageDependency(builder);

        builder.RegisterDependenciesInAssembly(ThisAssembly);
        AddJwt(builder);
    }

    private static void AddFileStorageDependency(ContainerBuilder builder)
    {
        builder.AddScopedAsSelf<FileSystemStorage>();
        builder.AddScopedAsImplementedInterfaces<FileStorageFactory>();
    }

    private static void AddJwt(ContainerBuilder builder)
    {
        builder.Register(context => context.Resolve<JwtLocalKeySyncService>())
            .InstancePerLifetimeScope();

        builder.RegisterType<JwtSecurityTokenHandler>().As<SecurityTokenHandler>();
    }
}