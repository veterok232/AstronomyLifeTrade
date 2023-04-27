using System.Collections.Concurrent;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Services.Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Jwt;

public class JwtKeyStorage : IJwtKeyStorage
{
    private readonly ConcurrentDictionary<Guid, JwtKeyModel> _keys = new();

    public IEnumerable<Guid> StoredKeyIds => _keys.Keys;

    public bool IsEmpty => _keys.IsEmpty;

    public bool RemoveKey(Guid keyId)
    {
        return _keys.Remove(keyId, out _);
    }

    public bool AddKey(Guid keyId, JwtKeyModel value)
    {
        return _keys.TryAdd(keyId, value);
    }

    public IEnumerable<SecurityKey> GetSecurityKeysForValidation()
    {
        return _keys.Where(k =>
                k.Value.EffectiveFrom < DateTime.UtcNow &&
                k.Value.ValidationEffectiveTo > DateTime.UtcNow)
            .Select(k => k.Value.SecurityKey);
    }

    public SecurityKey GetSecurityKeyForSign()
    {
        return _keys.First(k =>
                k.Value.EffectiveFrom < DateTime.UtcNow &&
                k.Value.SigningEffectiveTo > DateTime.UtcNow)
            .Value.SecurityKey;
    }
}