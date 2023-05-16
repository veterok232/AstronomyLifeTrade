using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Specifications.Common;
using ApplicationCore.Specifications.ImplicitFilters;

namespace Infrastructure.Specifications.Assignments;

internal class AssignmentForCreateSessionSpecification : Specification<Assignment>
{
    public AssignmentForCreateSessionSpecification(Guid assignmentId)
        : base(a => a.Id == assignmentId && a.Status == AssignmentStatus.Active)
    {
        AddIncludes(
            a => a.Role);

        SkipImplicitFilters(FilterKeys.Assignment);
    }
}