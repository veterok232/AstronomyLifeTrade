using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Entities.InitialData;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.Common;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.AccountProfile;
using ApplicationCore.Specifications.Users;
using ApplicationCore.Utils;

namespace ApplicationCore.Services.AccountProfile;

[ScopedDependency]
public class UserManagementService : IUserManagementService
{
    private readonly IRepository<Assignment> _assignmentRepository;
    private readonly IRepository<User> _usersRepository;
    private readonly IRepository<Session> _sessionsRepository;

    public UserManagementService(
        IRepository<Assignment> assignmentRepository,
        IRepository<User> usersRepository,
        IRepository<Session> sessionsRepository)
    {
        _assignmentRepository = assignmentRepository;
        _usersRepository = usersRepository;
        _sessionsRepository = sessionsRepository;
    }

    public async Task<Result> AssignAsManager(Guid userAssignmentId)
    {
        var assignment = await _assignmentRepository.GetSingleOrDefault(
            new AssignmentForManagementSpecification(userAssignmentId));
        
        if (assignment.Role.Name == Roles.Manager)
        {
            return ResultBuilder.BuildFailed("Пользователь уже является менеджером!");
        }

        assignment.RoleId = RolesInitData.ManagerRoleId;

        await _assignmentRepository.Update(assignment);

        return ResultBuilder.BuildSucceeded();
    }

    public async Task<Result> AssignAsAdministrator(Guid userAssignmentId)
    {
        var assignment = await _assignmentRepository.GetSingleOrDefault(
            new AssignmentForManagementSpecification(userAssignmentId));
        
        if (assignment.Role.Name == Roles.Staff)
        {
            return ResultBuilder.BuildFailed("Пользователь уже является администратором!");
        }

        assignment.RoleId = RolesInitData.StaffRoleId;

        await _assignmentRepository.Update(assignment);
        
        return ResultBuilder.BuildSucceeded();
    }

    public async Task<Result> BlockUser(Guid userAssignmentId)
    {
        var user = await _usersRepository.GetSingleOrDefault(
            new UserByAssignmentIdSpecification(userAssignmentId));

        if (user.LockedAt.HasValue)
        {
            return ResultBuilder.BuildFailed("Пользователь уже заблокирован!");
        }

        user.LockedAt = DateTime.UtcNow;

        await _usersRepository.Update(user);

        await InvalidateUserSessions(user.Id);
        
        return ResultBuilder.BuildSucceeded();
    }

    public async Task<Result> UnblockUser(Guid userAssignmentId)
    {
        var user = await _usersRepository.GetSingleOrDefault(
            new UserByAssignmentIdSpecification(userAssignmentId));
        
        if (!user.LockedAt.HasValue)
        {
            return ResultBuilder.BuildFailed("Пользователь не нуждается в разблокировке!");
        }

        user.LockedAt = null;

        await _usersRepository.Update(user);

        return ResultBuilder.BuildSucceeded();
    }

    private async Task InvalidateUserSessions(Guid userId)
    {
        var sessions = await _sessionsRepository.List(new SessionsForInvalidationSpecification(userId));

        foreach (var session in sessions)
        {
            await InvalidateSession(session);
        }
    }

    public Task InvalidateSession(Session session)
    {
        session.Invalidated = true;

        return _sessionsRepository.Update(session);
    }
}