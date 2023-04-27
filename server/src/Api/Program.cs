using Api.Constants;
using Api.Extensions;
using Api.Utils;
using ApplicationCore.Interfaces;
using Autofac.Extensions.DependencyInjection;
using NLog.Web;

namespace Api;

public sealed class Program
{
    private const long MaxRequestSizeInBytes = 52428800; // 50MB

    private static readonly string _clientDistPath = EnvironmentVariable.IsDev
        ? $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}client{Path.DirectorySeparatorChar}dist"
        : "client_dist";

    /// <summary>
    ///     ASP.NET Core entry point method which creates and run application host.
    /// </summary>
    /// <param name="args">The command line args used in configuration.</param>
    /// <returns>Exit code. Returns 0 (zero) to indicate that the process completed successfully, otherwise 1.</returns>
    public static async Task<int> Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        return await LogAndRun(host);
    }

    /// <summary>
    ///     Start <see cref="IHost" /> with safe logging.
    /// </summary>
    /// <param name="webHost"><see cref="IHost" />.</param>
    /// <returns>Exit code.</returns>
    private static async Task<int> LogAndRun(IHost webHost)
    {
        var logger = webHost.Services.GetService<IAppLogger<Program>>();
        try
        {
            logger.Info("Starting application.");

            await webHost.RunAsync();

            logger.Info("Stopped application.");

            return ProgramExitCodes.Success;
        }
        finally
        {
            if (EnvironmentVariable.IsDev)
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
            else
            {
                // wait for app insights flush
                // should be removed when https://github.com/microsoft/ApplicationInsights-dotnet/issues/1548 will be fixed
                Thread.Sleep(new TimeSpan(0, 0, seconds: 31));
            }
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseStartup<Startup>()
                    .UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBodySize = MaxRequestSizeInBytes;
                    })
                    .UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), _clientDistPath));
            })
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddAppConfiguration(context.HostingEnvironment.EnvironmentName, args);
            })
            .ConfigureLogging(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Trace);
                if (EnvironmentVariable.IsDev)
                {
                    logging.AddNLog("nlog.config");
                }
            });
    }
}