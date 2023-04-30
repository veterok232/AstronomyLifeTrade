using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Interfaces.Assignments;

public interface IAssignmentService
{
    Task<Assignment> GetByUser(Guid userId);

    Task<bool> Exist(Guid assignmentId);

    Task<Assignment> Find(ISpecification<Assignment> specification);
}