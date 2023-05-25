using ApplicationCore.Specifications.Common;
using Infrastructure.Entities;

namespace Infrastructure.Specifications;

internal class ActiveJwtKeysSpecification : Specification<JwtKey>
{
    public ActiveJwtKeysSpecification()
        : base(jsk => jsk.EffectiveFrom < DateTime.UtcNow &&
                      DateTime.UtcNow <= jsk.SigningEffectiveTo)
    {
    }
}