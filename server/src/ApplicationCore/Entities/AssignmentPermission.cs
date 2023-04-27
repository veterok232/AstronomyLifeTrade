namespace ApplicationCore.Entities;

public class AssignmentPermission : Entity
{
    public Guid AssignmentId { get; set; }

    public Guid PermissionId { get; set; }

    public Permission Permission { get; set; }
}