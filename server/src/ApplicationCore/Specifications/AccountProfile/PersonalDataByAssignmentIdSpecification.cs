using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.AccountProfile;

public class PersonalDataByAssignmentIdSpecification : Specification<PersonalData>
{
    public PersonalDataByAssignmentIdSpecification(Guid assignmentId)
        : base(pd => pd.AssignmentId == assignmentId)
    {
        AddInclude(pd => pd.Address);
    }
}