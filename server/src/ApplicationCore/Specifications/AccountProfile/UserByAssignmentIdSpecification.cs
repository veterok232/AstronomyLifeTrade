using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.AccountProfile;

public class UserByAssignmentIdSpecification : Specification<User>
{
    public UserByAssignmentIdSpecification(Guid assignmentId)
        : base(u => u.Assignment.Id == assignmentId)
    {
    }
}