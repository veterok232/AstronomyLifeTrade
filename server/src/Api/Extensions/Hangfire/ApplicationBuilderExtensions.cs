using Api.Constants;
using Hangfire;

namespace Api.Extensions.Hangfire;

internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds Hangfire dashboard in readonly mode without authentication.
    /// Should be used for testing purposes only.
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseHangfireDashboard(Routes.Hangfire, new DashboardOptions
        {
            Authorization = new[] { new PassThroughDashboardAuthorizationFilter() },
            IgnoreAntiforgeryToken = true,
            IsReadOnlyFunc = context => false,
        });

        return applicationBuilder;
    }
}