using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace Infrastructure.Specifications;

internal class SessionsForInvalidationSpecification : Specification<Session>
{
    public SessionsForInvalidationSpecification(Guid userId)
        : base(s => s.UserId == userId && s.ExpiryDate > DateTime.UtcNow && !s.Invalidated)
    {
    }
}