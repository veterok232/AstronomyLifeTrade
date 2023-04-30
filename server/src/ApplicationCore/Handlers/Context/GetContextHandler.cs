using System.Globalization;
using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Assignments;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Specifications;
using MediatR;

namespace ApplicationCore.Handlers.Context;

internal class GetContextHandler : IRequestHandler<Query<ContextResponse>, ContextResponse>
{
    private readonly IAuthContextAccessor _authContextAccessor;
    private readonly IAssignmentService _assignmentService;
    private readonly IRepository<Session> _sessionRepository;

    public GetContextHandler(
        IAuthContextAccessor authContextAccessor,
        IAssignmentService assignmentService,
        IRepository<Session> sessionRepository)
    {
        _authContextAccessor = authContextAccessor;
        _assignmentService = assignmentService;
        _sessionRepository = sessionRepository;
    }

    private Guid? AssignmentIdFromContext => _authContextAccessor.AssignmentId;

    private Guid? UserIdFromContext => _authContextAccessor.UserId;

    /// <inheritdoc />
    public async Task<ContextResponse> Handle(Query<ContextResponse> request, CancellationToken cancellationToken)
    {
        Assignment userAssignment = UserIdFromContext.HasValue
            ? await _assignmentService.GetByUser(UserIdFromContext.Value)
            : null;
        
        var user = userAssignment?.User;

        return new ContextResponse
        {
            IsAuthenticated = _authContextAccessor.IsAuthenticated,
            UserId = user?.Id,
            FirstName = user?.FirstName,
            LastName = user?.LastName,
            RoleName = userAssignment?.Role?.Name,
            Lang = CultureInfo.CurrentCulture.Name,
            RefreshTokenExpirationDateTime = await GetSessionExpirationDate(),
        };
    }

    private async Task<DateTime?> GetSessionExpirationDate()
    {
        var sessionId = _authContextAccessor.SessionId;
        if (sessionId == null)
        {
            return null;
        }

        return await _sessionRepository.GetSingleOrDefault(new SessionExpirationDateSpecification(sessionId.Value));
    }
}