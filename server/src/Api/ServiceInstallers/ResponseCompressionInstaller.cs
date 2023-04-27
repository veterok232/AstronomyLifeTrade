using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

namespace Api.ServiceInstallers;

internal class ResponseCompressionInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddResponseCompression(o =>
        {
            o.Providers.Add<GzipCompressionProvider>();
            o.MimeTypes = GetCompressibleMimeTypes();
        });
        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });
    }

    private static IEnumerable<string> GetCompressibleMimeTypes()
    {
        return ResponseCompressionDefaults.MimeTypes.Concat(
            new[]
            {
                "image/svg+xml",
                "application/x-font-ttf",
                "image/x-icon",
            });
    }
}