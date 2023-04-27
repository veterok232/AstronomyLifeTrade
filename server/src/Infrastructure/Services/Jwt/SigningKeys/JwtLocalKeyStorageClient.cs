using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Jwt.SigningKeys;

[ScopedDependency]
internal class JwtLocalKeyStorageClient : IJwtLocalKeyStorageClient
{
    private readonly IJwtKeyParametersSerializer _jwtKeyParametersSerializer;
    private readonly JwtSettings _jwtSettings;

    public JwtLocalKeyStorageClient(
        IJwtKeyParametersSerializer jwtKeyParametersSerializer,
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtKeyParametersSerializer = jwtKeyParametersSerializer;
        _jwtSettings = jwtSettings.Value;
    }

    public RsaSecurityKey Get()
    {
        var rsaParameters = _jwtKeyParametersSerializer.Deserialize(
            _jwtSettings.SigningKeysSettings.LocalKey);

        return new RsaSecurityKey(rsaParameters)
        {
            KeyId = nameof(JwtSettings.SigningKeysSettings.LocalKey),
        };
    }
}