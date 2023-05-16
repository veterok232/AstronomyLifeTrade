using ApplicationCore.Entities;

namespace Infrastructure.Entities;

public class JwtKey : Entity
{
    public Guid SecretId { get; set; }

    public SecretToken SecretToken { get; set; }

    public DateTime EffectiveFrom { get; set; }

    public DateTime SigningEffectiveTo { get; set; }

    public DateTime ValidationEffectiveTo { get; set; }
}