using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Specifications.Common;
using ApplicationCore.Utils;

namespace ApplicationCore.Specifications.Users;

internal class UserForDuplicatesCheckSpecification : Specification<User>
{
    public UserForDuplicatesCheckSpecification(string email)
        : base(u => u.Email == email)
    {
    }
}