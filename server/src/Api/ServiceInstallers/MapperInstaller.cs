using AutoMapper;
using ApplicationCore;
using Infrastructure;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Api.ServiceInstallers;

public class MapperInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfiles(new[] { new ApiMappingProfile() });
            cfg.AddMaps(
                typeof(ApplicationCoreModule).Assembly,
                typeof(InfrastructureModule).Assembly);
        });

        services.AddSingleton<IConfigurationProvider>(sp => mapperConfiguration);
        services.Add(new ServiceDescriptor(
            typeof(IMapper),
            sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService),
            ServiceLifetime.Transient));
    }
}