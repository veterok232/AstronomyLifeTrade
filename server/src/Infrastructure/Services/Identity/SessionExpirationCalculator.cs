using ApplicationCore.Entities;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Identity;

[ScopedDependency]
internal class SessionExpirationCalculator : ISessionExpirationCalculator
{
    private readonly JwtSettings _jwtSettings;

    public SessionExpirationCalculator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public DateTime GetExpirationTime(Assignment assignment)
    {
        if (assignment == null)
        {
            return GetDefaultExpirationTime();
        }

        return GetDefaultExpirationTime();
    }

    private DateTime GetDefaultExpirationTime()
    {
        return DateTime.UtcNow.Add(_jwtSettings.RefreshTokenLifetime);
    }
}