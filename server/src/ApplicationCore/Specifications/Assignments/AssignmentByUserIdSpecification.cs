using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;
using ApplicationCore.Specifications.ImplicitFilters;

namespace ApplicationCore.Specifications.Assignments;

internal class AssignmentByUserIdSpecification : Specification<Assignment>
{
    public AssignmentByUserIdSpecification(Guid userId)
        : base(p =>
            p.User.Id == userId)
    {
        AddIncludes();
        ApplyOrderBy(a => a.CreatedAt);
    }

    private void AddIncludes()
    {
        AddInclude(p => p.User);
        AddInclude(p => p.Role);
    }
}