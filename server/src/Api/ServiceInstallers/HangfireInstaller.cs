using Api.Extensions.Hangfire;
using Api.Services.JsonConverters;
using Hangfire;
using Hangfire.PostgreSql;
using Newtonsoft.Json;

namespace Api.ServiceInstallers;

internal class HangfireInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsBackgroundProcessingEnabled() is false)
        {
            return;
        }

        services.AddHangfire((provider, config) =>
        {
            config.UsePostgreSqlStorage(configuration.GetConnectionString("Hangfire"));
            config.UseFilter(new AutomaticRetryAttribute
            {
                Attempts = 0,
                OnAttemptsExceeded = AttemptsExceededAction.Delete,
            });
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new DateOnlyJsonConverter());
            config.UseSerializerSettings(settings);
        });
    }
}