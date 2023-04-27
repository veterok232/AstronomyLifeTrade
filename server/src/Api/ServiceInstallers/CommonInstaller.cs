using Api.Controllers.Filters;
using Api.Controllers.Transformers;
using Api.Extensions;
using Api.Services.Binders;
using ApplicationCore.Services.JsonConverters;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.ServiceInstallers;

internal class CommonInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });
        services.AddHsts(options =>
        {
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(730);
            options.Preload = true;
        });
        services
            .AddControllers(options => options.ModelBinderProviders.Insert(0, new CommonModelBinderProvider()))
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new StringTrimmingJsonConverter());
            });

        services.AddMemoryCache();
        services.AddSettings(configuration);

        services.AddMvc(options =>
        {
            options.Filters.Add<LocalizationFilter>();
            options.Filters.Add<DataAuthorizationFilter>();
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });
    }
}