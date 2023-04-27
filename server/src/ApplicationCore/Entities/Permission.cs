namespace ApplicationCore.Entities;

public class Permission : Entity
{
    public Guid? ParentPermissionId { get; set; }

    public Permission ParentPermission { get; set; }

    public string Name { get; set; }
}