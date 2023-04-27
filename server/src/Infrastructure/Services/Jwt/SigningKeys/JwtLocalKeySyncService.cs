using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Services.Jwt.Models;

namespace Infrastructure.Services.Jwt.SigningKeys;

[SelfScopedDependency]
internal class JwtLocalKeySyncService : IJwtKeySyncService
{
    private readonly IJwtLocalKeyStorageClient _jwtKeyStorageClient;
    private readonly IJwtKeyStorage _jwtKeyStorage;

    public JwtLocalKeySyncService(IJwtLocalKeyStorageClient jwtKeyStorageClient, IJwtKeyStorage jwtKeyStorage)
    {
        _jwtKeyStorageClient = jwtKeyStorageClient;
        _jwtKeyStorage = jwtKeyStorage;
    }

    public Task Sync()
    {
        if (_jwtKeyStorage.IsEmpty)
        {
            _jwtKeyStorage.AddKey(
                Guid.Empty,
                new JwtKeyModel(
                    securityKey: _jwtKeyStorageClient.Get(),
                    effectiveFrom: DateTime.UtcNow,
                    signingEffectiveTo: DateTime.MaxValue,
                    validationEffectiveTo: DateTime.MaxValue));
        }

        return Task.CompletedTask;
    }
}