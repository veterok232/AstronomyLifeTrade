using Api.Extensions.Hangfire;
using Hangfire.Server;
using Infrastructure.Data.DatabaseSetup;
using Infrastructure.Services.Jwt.SigningKeys;
using Infrastructure.Services.Storage.Files;

namespace Api.BackgroundProcessing;

internal static class StartupJobs
{
    public static async Task RunStartupJobs(this IServiceProvider services)
    {
        services.GetService<FileSystemCacheSetup>()?.Run();
        services.GetService<DatabaseSetup>()?.Run();
        await services.GetService<JwtKeySetup>().Run();
    }
}