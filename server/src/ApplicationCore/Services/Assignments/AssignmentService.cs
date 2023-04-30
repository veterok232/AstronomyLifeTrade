using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Assignments;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Assignments;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Services.Assignments;

[ScopedDependency]
public class AssignmentService : IAssignmentService
{
    private readonly IRepository<Assignment> _assignmentRepository;

    public AssignmentService(
        IRepository<Assignment> assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public Task<Assignment> GetByUser(Guid userId) =>
        _assignmentRepository.GetSingleOrDefault(new AssignmentByUserIdSpecification(userId));
    

    public Task<Assignment> Find(ISpecification<Assignment> specification)
    {
        return _assignmentRepository.GetSingleOrDefault(specification);
    }

    public Task<bool> Exist(Guid assignmentId)
    {
        return _assignmentRepository.Any(new Specification<Assignment>(a => a.Id == assignmentId));
    }
}