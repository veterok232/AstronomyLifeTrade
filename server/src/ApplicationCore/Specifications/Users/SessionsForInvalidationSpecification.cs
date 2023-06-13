using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Users;

public class SessionsForInvalidationSpecification : Specification<Session>
{
    public SessionsForInvalidationSpecification(Guid userId)
        : base(s => s.UserId == userId && !s.Invalidated)
    {
    }
}