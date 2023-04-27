using Infrastructure.Settings;

namespace Api.Extensions.Hangfire;

internal static class ConfigurationExtensions
{
    public static bool IsBackgroundProcessingEnabled(this IConfiguration configuration)
    {
        return configuration.GetSection(nameof(BackgroundProcessingSettings))
            .Get<BackgroundProcessingSettings>()
            .IsEnabled;
    }
}