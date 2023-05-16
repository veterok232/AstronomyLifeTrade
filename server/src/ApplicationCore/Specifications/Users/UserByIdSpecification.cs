using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Users;

public class UserByIdSpecification : Specification<User>
{
    public UserByIdSpecification(Guid userId)
        : base(p => p.Id == userId)
    {
        AddInclude(p => p.Assignment);
        AddInclude(p => p.Assignment.Role);
    }
}