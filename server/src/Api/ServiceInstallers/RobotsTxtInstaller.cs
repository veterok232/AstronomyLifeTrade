using Api.Constants;
using RobotsTxt;

namespace Api.ServiceInstallers;

internal class RobotsTxtInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration[EnvironmentVariableNames.EnvironmentType] == Environments.Production)
        {
            services.AddStaticRobotsTxt(builder =>
                builder.AddSection(section =>
                    section.AddUserAgent("*")
                        .Allow("/$")
                        .Disallow("/")));
        }
        else
        {
            services.AddStaticRobotsTxt(builder =>
                builder.AddSection(section =>
                    section.AddUserAgent("*")
                        .Disallow("/")));
        }
    }
}