using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Users;

public class ActiveUserByEmailForLoginSpecification : Specification<User>
{
    public ActiveUserByEmailForLoginSpecification(string email)
        : base(u =>
            u.Email == email)
    {
        AddInclude(u => u.Assignment);
    }
}