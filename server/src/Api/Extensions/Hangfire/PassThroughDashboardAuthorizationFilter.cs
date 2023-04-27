using Hangfire.Dashboard;

namespace Api.Extensions.Hangfire;

internal class PassThroughDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}