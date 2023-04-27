namespace ApplicationCore.Entities.InitialData;

public static class RolesInitData
{
    public static readonly Guid StaffRoleId = new Guid("04e7d93f-8c78-47cb-a56f-6244a0a0fc56");
    public static readonly Guid ManagerRoleId = new Guid("55e1b1f5-0a7b-49c1-8d61-5a20e93111dd");
    public static readonly Guid ConsumerRoleId = new Guid("cd7be9d2-7898-4673-8698-672d07594ec8");
    public static readonly Guid SystemRoleId = new Guid("a8aecf8e-1dba-497f-abdd-74db9384398e");

    public static readonly Role[] Data =
    {
        new Role { Id = StaffRoleId, Name = Constants.Roles.Staff },
        new Role { Id = ManagerRoleId, Name = Constants.Roles.Manager },
        new Role { Id = ConsumerRoleId, Name = Constants.Roles.Consumer },
        new Role { Id = SystemRoleId, Name = Constants.Roles.System },
    };
}