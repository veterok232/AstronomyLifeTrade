using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace Infrastructure.Specifications;

internal class SessionForAuthorizationSpecification
    : Specification<Session>
{
    public SessionForAuthorizationSpecification(Guid refreshToken)
        : base(t => t.RefreshToken == refreshToken)
    {
        AddIncludes(
            t => t.User,
            t => t.Assignment);

        AddInclude($"{nameof(Session.Assignment)}.{nameof(Assignment.Role)}");
    }
}