using Infrastructure.BackgroundJobs;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Services.Jwt;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IdentityConstants = Infrastructure.Constants.IdentityConstants;

namespace Api.ServiceInstallers;

internal class JwtInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        IJwtKeyStorage keyStorage = new JwtKeyStorage();
        services.AddSingleton(keyStorage);

        var jwtSettings = ReadJwtSettings(services, configuration);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKeyResolver = (_, _, _, _) => keyStorage.GetSecurityKeysForValidation(),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = IdentityConstants.ClockSkew,
        };

        // configure jwt authentication
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

        if (jwtSettings.SigningKeysSettings.EnableRotation)
        {
            services.AddHostedService<JwtKeysSyncBackgroundJob>();
        }
    }

    private static JwtSettings ReadJwtSettings(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
        services.Configure<JwtSettings>(jwtSettingsSection);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        return jwtSettings;
    }
}