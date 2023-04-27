namespace Api.Extensions;

internal static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddAppConfiguration(
        this IConfigurationBuilder configurationBuilder,
        string hostingEnvironmentName,
        string[] args)
    {
        var isDev = hostingEnvironmentName == Environments.Development;

        configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory()) // or similar
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        if (isDev)
        {
            configurationBuilder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
        }

        return configurationBuilder;
    }
}