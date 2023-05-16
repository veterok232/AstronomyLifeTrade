using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Jwt.SigningKeys;

/*[SelfScopedDependency]
public class JwtKeySetup
{
    private readonly IRepository<JwtKey> _jwtKeyRepository;
    private readonly Lazy<IJwtKeyCreationService> _jwtKeyCreationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtKeySyncService _jwtKeySyncService;

    public JwtKeySetup(
        IRepository<JwtKey> jwtKeyRepository,
        Lazy<IJwtKeyCreationService> jwtKeyCreationService,
        IUnitOfWork unitOfWork,
        IOptions<JwtSettings> jwtSettings,
        IJwtKeySyncService jwtKeySyncService)
    {
        _jwtKeyRepository = jwtKeyRepository;
        _jwtKeyCreationService = jwtKeyCreationService;
        _unitOfWork = unitOfWork;
        _jwtKeySyncService = jwtKeySyncService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task Run()
    {
        if (await IsInitKeyNeeded())
        {
            await _jwtKeyCreationService.Value.Create();
            await _unitOfWork.Commit();
        }

        await _jwtKeySyncService.Sync();
    }

    private async Task<bool> IsInitKeyNeeded()
    {
        return _jwtSettings.SigningKeysSettings.EnableRotation &&
               !await _jwtKeyRepository.Any(new ActiveJwtKeysSpecification());
    }
}*/