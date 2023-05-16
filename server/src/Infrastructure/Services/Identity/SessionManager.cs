using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Infrastructure.Specifications.Assignments;

namespace Infrastructure.Services.Identity;

[ScopedDependency]
internal class SessionManager : ISessionManager
{
    private readonly IRepository<Session> _sessionRepository;
    private readonly IRepository<Assignment> _assignmentRepository;
    private readonly IRepository<User> _userRepository;
    private readonly ISessionExpirationCalculator _expirationCalculator;

    public SessionManager(
        IRepository<Session> sessionRepository,
        ISessionExpirationCalculator expirationCalculator,
        IRepository<Assignment> assignmentRepository,
        IRepository<User> userRepository)
    {
        _sessionRepository = sessionRepository;
        _expirationCalculator = expirationCalculator;
        _assignmentRepository = assignmentRepository;
        _userRepository = userRepository;
    }

    public async Task<Session> CreateSession(User user, string fingerprint)
    {
        var assignmentId = user.Assignment?.Id;
        var assignment = assignmentId != null
            ? await _assignmentRepository.GetSingleOrDefault(
                new AssignmentForCreateSessionSpecification(assignmentId.Value))
            : null;

        return await CreateSession(user, assignment, fingerprint);
    }

    public async Task<Session> CreateSession(Guid assignmentId, string fingerprint, Guid? originAssignmentId = null)
    {
        var assignment = await _assignmentRepository.GetSingleOrDefault(
            new AssignmentForCreateSessionSpecification(assignmentId));
        var user = await _userRepository.GetById(assignment.UserId);

        return await CreateSession(user, assignment, fingerprint, originAssignmentId);
    }

    public Task<Session> UpdateRefreshToken(Session session)
    {
        session.RefreshToken = Guid.NewGuid();
        return _sessionRepository.Update(session);
    }

    private async Task<Session> CreateSession(
        User user, Assignment assignment, string fingerprint, Guid? originAssignmentId = null)
    {
        return await _sessionRepository.Add(
            new Session
            {
                CreationDate = DateTime.UtcNow,
                ExpiryDate = _expirationCalculator.GetExpirationTime(assignment),
                User = user,
                UserId = user.Id,
                Assignment = assignment,
                AssignmentId = assignment?.Id,
                Fingerprint = fingerprint,
                RefreshToken = Guid.NewGuid(),
            });
    }

    public Task ExtendSession(Session session)
    {
        session.ExpiryDate = _expirationCalculator.GetExpirationTime(session.Assignment);

        return _sessionRepository.Update(session);
    }

    public async Task InvalidateUserSessions(Guid userId)
    {
        var sessions = await _sessionRepository.List(new SessionsForInvalidationSpecification(userId));

        foreach (var session in sessions)
        {
            await InvalidateSession(session);
        }
    }

    public Task InvalidateSession(Session session)
    {
        session.Invalidated = true;

        return _sessionRepository.Update(session);
    }
}