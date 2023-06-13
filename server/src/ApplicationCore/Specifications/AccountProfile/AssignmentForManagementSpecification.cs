using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.AccountProfile;

public class AssignmentForManagementSpecification : Specification<Assignment>
{
    public AssignmentForManagementSpecification(Guid assignmentId)
        : base(a => a.Id == assignmentId)
    {
        AddInclude(a => a.Role);
    }
}