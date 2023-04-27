namespace ApplicationCore.Entities.Interfaces;

public interface IHasCreatedByAssignment
{
    Guid CreatedByAssignmentId { get; set; }
}