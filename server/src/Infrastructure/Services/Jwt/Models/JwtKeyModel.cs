using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Jwt.Models;

public class JwtKeyModel
{
    public JwtKeyModel(
        SecurityKey securityKey,
        DateTime effectiveFrom,
        DateTime signingEffectiveTo,
        DateTime validationEffectiveTo)
    {
        SecurityKey = securityKey;
        EffectiveFrom = effectiveFrom;
        SigningEffectiveTo = signingEffectiveTo;
        ValidationEffectiveTo = validationEffectiveTo;
    }

    public SecurityKey SecurityKey { get; }

    public DateTime EffectiveFrom { get; }

    public DateTime SigningEffectiveTo { get; }

    public DateTime ValidationEffectiveTo { get; }
}