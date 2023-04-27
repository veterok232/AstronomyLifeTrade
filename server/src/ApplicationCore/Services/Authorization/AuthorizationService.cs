using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Authorization;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Authorization;

[ScopedDependency]
internal class AuthorizationService : IAuthorizationService
{
    private readonly IAuthContextAccessor _authContextAccessor;

    public AuthorizationService(IAuthContextAccessor authContextAccessor)
    {
        _authContextAccessor = authContextAccessor;
    }

    public bool IsAuthorized(string privilege)
    {
        return _authContextAccessor.Privileges.Contains(privilege);
    }
}