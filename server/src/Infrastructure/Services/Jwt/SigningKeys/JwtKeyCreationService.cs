using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Settings;
using Infrastructure.Specifications;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Jwt.SigningKeys;

[ScopedDependency]
internal class JwtKeyCreationService : IJwtKeyCreationService
{
    private const int RsaKeySizeInBits = 2048;

    private readonly IRepository<JwtKey> _jwtKeyRepository;
    private readonly JwtSettings _jwtSettings;

    public JwtKeyCreationService(
        IRepository<JwtKey> jwtKeyRepository,
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtKeyRepository = jwtKeyRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task Create()
    {
        var jwtKeyEntity = await CreateJwtKeyEntity();
    }

    private async Task<JwtKey> CreateJwtKeyEntity()
    {
        var newKeySigningEffectiveFrom =
            await _jwtKeyRepository.Max(new JwtKeyMaxSigningEffectiveToSpecification()) ??
            DateTime.UtcNow;

        return await _jwtKeyRepository.Add(new JwtKey
        {
            EffectiveFrom = newKeySigningEffectiveFrom,
            SigningEffectiveTo = newKeySigningEffectiveFrom.AddDays(
                _jwtSettings.SigningKeysSettings.KeySignLifetimeInDays),
            ValidationEffectiveTo = newKeySigningEffectiveFrom.AddDays(
                _jwtSettings.SigningKeysSettings.KeyValidationLifetimeInDays),
            SecretToken = new SecretToken
            {
                IsAttached = true,
            },
        });
    }
}