namespace Api.ServiceInstallers;

internal interface IServiceInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}