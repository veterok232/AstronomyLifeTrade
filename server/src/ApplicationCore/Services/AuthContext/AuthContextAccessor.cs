using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.AuthContext;

[ScopedDependency]
internal class AuthContextAccessor : IAuthContextAccessor
{
    private readonly IAuthenticatedUserContext _authenticatedUserContext;
    private IAuthenticatedUserContext _overridenContext;

    public AuthContextAccessor(IAuthenticatedUserContext authenticatedUserContext)
    {
        _authenticatedUserContext = authenticatedUserContext;
    }

    private IAuthenticatedUserContext CurrentContext => _overridenContext ?? _authenticatedUserContext;

    public Guid? UserId => CurrentContext.UserId;

    public Guid? AssignmentId => CurrentContext.AssignmentId;

    public string RoleName => CurrentContext.RoleName;

    public bool IsAuthenticated => CurrentContext.IsAuthenticated;

    public Guid? OriginAssignmentId => CurrentContext.OriginAssignmentId;

    public Guid? SessionId => CurrentContext.SessionId;

    public IEnumerable<string> Privileges => CurrentContext.Privileges;

    public void SetContext(IAuthenticatedUserContext context)
    {
        if (_authenticatedUserContext.IsInitialized)
        {
            throw new InvalidOperationException("Attempt to override existing authenticated user context");
        }

        _overridenContext = context;
    }
}