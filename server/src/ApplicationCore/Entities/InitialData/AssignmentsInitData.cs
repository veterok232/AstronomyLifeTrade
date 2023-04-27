using ApplicationCore.Enums;

namespace ApplicationCore.Entities.InitialData;

public static class AssignmentsInitData
{
    public static readonly Guid SystemAssignmentId = new Guid("348a8f47-a4f5-4ac0-8c3c-282c6b03118d");

    public static readonly Assignment[] Data =
    {
        new Assignment
        {
            Id = SystemAssignmentId,
            RoleId = RolesInitData.SystemRoleId,
            UserId = UsersInitData.SystemUserId,
            Status = AssignmentStatus.Active,
            CreatedByUserId = UsersInitData.SystemUserId,
            CreatedAt = new DateTime(2020, 11, 09, 0, 0, 0, DateTimeKind.Utc),
            Phone = "+11",
        },
    };
}