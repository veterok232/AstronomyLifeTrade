using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Services.Jwt.Models;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Jwt.SigningKeys;

[SelfScopedDependency]
internal class JwtLocalKeySyncService : IJwtKeySyncService
{
    private readonly IJwtKeyStorage _jwtKeyStorage;
    private readonly IJwtKeyParametersSerializer _jwtKeyParametersSerializer;
    private readonly JwtSettings _jwtSettings;

    public JwtLocalKeySyncService(
        IJwtKeyStorage jwtKeyStorage,
        IJwtKeyParametersSerializer jwtKeyParametersSerializer,
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtKeyStorage = jwtKeyStorage;
        _jwtKeyParametersSerializer = jwtKeyParametersSerializer;
        _jwtSettings = jwtSettings.Value;
    }

    public Task Sync()
    {
        if (_jwtKeyStorage.IsEmpty)
        {
            _jwtKeyStorage.AddKey(
                Guid.Empty,
                new JwtKeyModel(
                    securityKey: GetSecurityKey(),
                    effectiveFrom: DateTime.UtcNow,
                    signingEffectiveTo: DateTime.MaxValue,
                    validationEffectiveTo: DateTime.MaxValue));
        }

        return Task.CompletedTask;
    }

    private RsaSecurityKey GetSecurityKey()
    {
        var rsaParameters = _jwtKeyParametersSerializer.Deserialize(
            _jwtSettings.SigningKeysSettings.LocalKey);

        return new RsaSecurityKey(rsaParameters)
        {
            KeyId = nameof(JwtSettings.SigningKeysSettings.LocalKey),
        };
    }
}