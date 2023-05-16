using ApplicationCore.Entities;

namespace Infrastructure.Interfaces;

internal interface ISessionManager
{
    Task<Session> CreateSession(User user, string fingerprint);

    Task<Session> CreateSession(Guid assignmentId, string fingerprint, Guid? originAssignmentId = null);

    Task<Session> UpdateRefreshToken(Session session);

    Task ExtendSession(Session session);

    Task InvalidateUserSessions(Guid userId);

    Task InvalidateSession(Session session);
}