using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Users;

public class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(string email)
        : base(u => u.Email == email)
    {
        AddInclude(p => p.Assignment);
        AddInclude(p => p.Assignment.Role);
    }
}