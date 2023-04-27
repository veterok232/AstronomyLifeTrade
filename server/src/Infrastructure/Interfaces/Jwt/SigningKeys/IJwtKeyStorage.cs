using Infrastructure.Services.Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Interfaces.Jwt.SigningKeys;

public interface IJwtKeyStorage
{
    IEnumerable<Guid> StoredKeyIds { get; }

    bool IsEmpty { get; }

    bool RemoveKey(Guid keyId);

    bool AddKey(Guid keyId, JwtKeyModel value);

    IEnumerable<SecurityKey> GetSecurityKeysForValidation();

    SecurityKey GetSecurityKeyForSign();
}