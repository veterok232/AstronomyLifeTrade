using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Interfaces.Jwt.SigningKeys;

internal interface IJwtLocalKeyStorageClient
{
    RsaSecurityKey Get();
}