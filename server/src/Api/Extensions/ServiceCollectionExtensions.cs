using ApplicationCore.Settings;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Api.Extensions;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
    /// Objects (POCO) and adding <see cref="IOptions{TOptions}" /> objects to the services collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" />.</param>
    /// <param name="configuration">The <see cref="IConfiguration" />.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional invocations can be chained.</returns>
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SpaRoutesSettings>(configuration.GetSection(nameof(SpaRoutesSettings)));
        services.Configure<ResourcesSettings>(configuration.GetSection(nameof(ResourcesSettings)));
        services.Configure<FileStorageSettings>(configuration.GetSection(nameof(FileStorageSettings)));
        services.Configure<LocalStorageSettings>(configuration.GetSection(nameof(LocalStorageSettings)));
        services.Configure<AppInfoSettings>(configuration.GetSection(nameof(AppInfoSettings)));
        services.Configure<InitialSetupSettings>(configuration.GetSection(nameof(InitialSetupSettings)));
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
        services.Configure<FileStorageCacheSettings>(configuration.GetSection(nameof(FileStorageCacheSettings)));
        services.Configure<ExpiredSessionsSettings>(configuration.GetSection(nameof(ExpiredSessionsSettings)));
        services.Configure<OneTimeTokenAuthenticationSettings>(
            configuration.GetSection(nameof(OneTimeTokenAuthenticationSettings)));
        services.Configure<BackgroundProcessingSettings>(
            configuration.GetSection(nameof(BackgroundProcessingSettings)));
        services.Configure<CustomConfigurationSettings>(configuration.GetSection(nameof(CustomConfigurationSettings)));
        services.Configure<TemporaryFilesDeleteSettings>(configuration.GetSection(nameof(TemporaryFilesDeleteSettings)));

        return services;
    }
}