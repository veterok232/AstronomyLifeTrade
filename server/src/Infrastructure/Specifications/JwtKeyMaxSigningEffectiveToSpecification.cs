using ApplicationCore.Specifications.Common;
using Infrastructure.Entities;

namespace Infrastructure.Specifications;

internal class JwtKeyMaxSigningEffectiveToSpecification
    : DataTransformSpecification<JwtKey, DateTime?>
{
    public JwtKeyMaxSigningEffectiveToSpecification()
        : base(jsk => jsk.SigningEffectiveTo)
    {
    }
}