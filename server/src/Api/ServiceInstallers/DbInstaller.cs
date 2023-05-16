using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.ServiceInstallers;

internal class DbInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                    configuration.GetConnectionString("Default"),
                    o => o
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
                        .CommandTimeout(configuration.GetValue<int>("DbConnectionTimeoutInSeconds")))
                .UseSnakeCaseNamingConvention());

        services.AddIdentityCore<User>()
            .AddDefaultTokenProviders();
    }
}