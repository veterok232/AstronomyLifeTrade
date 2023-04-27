using Api.Settings;
using ApplicationCore.Extensions;

namespace Api.ServiceInstallers;

public class CorsInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var availableOrigins = configuration
            .GetSection(nameof(CorsSettings))
            ?.Get<CorsSettings>()
            ?.SplitAvailableOrigins();

        if (availableOrigins.IsNullOrEmpty())
        {
            return;
        }

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy => policy.WithOrigins(availableOrigins.ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
        });
    }
}