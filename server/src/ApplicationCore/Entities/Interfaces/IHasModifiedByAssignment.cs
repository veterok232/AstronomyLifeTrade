namespace ApplicationCore.Entities.Interfaces;

public interface IHasModifiedByAssignment
{
    Guid ModifiedByAssignmentId { get; set; }
}