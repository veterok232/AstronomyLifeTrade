using Api.Constants;
using Api.Middleware;
using Api.ServiceInstallers;
using ApplicationCore;
using ApplicationCore.Services.Dependencies.Extensions;
using Autofac;
using Infrastructure;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    public IConfiguration Configuration { get; }

    private IWebHostEnvironment Environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallServicesInAssembly(Configuration);
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        // Add any Autofac modules or registrations.
        // This is called AFTER ConfigureServices so things you
        // register here OVERRIDE things registered in ConfigureServices.
        //
        // You must have the call to `UseServiceProviderFactory(new AutofacServiceProviderFactory())`
        // when building the host or this won't be called.
        builder.RegisterDependenciesInAssembly(typeof(Startup).Assembly);
        builder.RegisterModule(new InfrastructureModule(Configuration));
        builder.RegisterModule<ApplicationCoreModule>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        app.UseResponseCompression();
        app.UseForwardedHeaders();

        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseMiddleware<SpaMiddleware>();

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Astronomy Life Trade API V1");
            c.RoutePrefix = Routes.Swagger.TrimStart('/');
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
