using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Services.Jwt.Models;
using Infrastructure.Settings;
using Infrastructure.Specifications;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Jwt.SigningKeys;

[SelfScopedDependency]
public class JwtKeySetup
{
    private readonly IRepository<JwtKey> _jwtKeyRepository;
    private readonly Lazy<IJwtKeyCreationService> _jwtKeyCreationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtKeyStorage _jwtKeyStorage;
    private readonly IJwtKeyParametersSerializer _jwtKeyParametersSerializer;

    public JwtKeySetup(
        IRepository<JwtKey> jwtKeyRepository,
        Lazy<IJwtKeyCreationService> jwtKeyCreationService,
        IUnitOfWork unitOfWork,
        IOptions<JwtSettings> jwtSettings,
        IJwtKeyStorage jwtKeyStorage,
        IJwtKeyParametersSerializer jwtKeyParametersSerializer)
    {
        _jwtKeyRepository = jwtKeyRepository;
        _jwtKeyCreationService = jwtKeyCreationService;
        _unitOfWork = unitOfWork;
        _jwtKeyStorage = jwtKeyStorage;
        _jwtKeyParametersSerializer = jwtKeyParametersSerializer;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task Run()
    {
        if (await IsInitKeyNeeded())
        {
            await _jwtKeyCreationService.Value.Create();
            await _unitOfWork.Commit();
        }

        await Sync();
    }

    private async Task<bool> IsInitKeyNeeded()
    {
        return _jwtSettings.SigningKeysSettings.EnableRotation &&
               !await _jwtKeyRepository.Any(new ActiveJwtKeysSpecification());
    }

    private Task Sync()
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