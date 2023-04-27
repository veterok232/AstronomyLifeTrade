namespace ApplicationCore.Interfaces.Assignments;

public interface IAssignmentService
{
    Task<int> GetCountByUser(Guid userId);
}