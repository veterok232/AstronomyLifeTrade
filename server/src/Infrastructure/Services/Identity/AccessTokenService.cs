using System.Security.Claims;
using ApplicationCore.Entities;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Identity;

[ScopedDependency]
internal class AccessTokenService : IAccessTokenService
{
    private readonly IClaimsService _claimsService;
    private readonly SecurityTokenHandler _tokenHandler;
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtKeyStorage _jwtKeyStorage;

    public AccessTokenService(
        IClaimsService claimsService,
        IOptions<JwtSettings> jwtSettings,
        SecurityTokenHandler tokenHandler,
        IJwtKeyStorage jwtKeyStorage)
    {
        _claimsService = claimsService;
        _tokenHandler = tokenHandler;
        _jwtKeyStorage = jwtKeyStorage;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> CreateToken(Session session)
    {
        var claims = await _claimsService.CreateClaims(session);
        var tokenDescriptor = CreateSecurityTokenDescriptor(claims);
        var token = _tokenHandler.CreateToken(tokenDescriptor);

        return _tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor CreateSecurityTokenDescriptor(IEnumerable<Claim> claims)
    {
        var currentDateTimeAsUtc = DateTime.UtcNow;

        return new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            NotBefore = currentDateTimeAsUtc,
            IssuedAt = currentDateTimeAsUtc,
            Subject = new ClaimsIdentity(claims),
            Expires = currentDateTimeAsUtc.Add(_jwtSettings.TokenLifetime),
            SigningCredentials =
                new SigningCredentials(_jwtKeyStorage.GetSecurityKeyForSign(), SecurityAlgorithms.RsaSha512),
        };
    }
}