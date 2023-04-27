using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Constants;
using Api.Extensions;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Services.Dependencies.Attributes;

namespace Api.Accessors;

[ScopedDependency]
public class WebAuthUserContext : IAuthenticatedUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WebAuthUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId => _httpContextAccessor.HttpContext.GetUserId();

    public Guid? AssignmentId => _httpContextAccessor.HttpContext.GetAssignmentId();

    public string RoleName => _httpContextAccessor.HttpContext.GetClaim<string>(ClaimTypes.Role);

    public bool IsAuthenticated => _httpContextAccessor.HttpContext.GetClaimsIdentity().IsAuthenticated;

    public Guid? OriginAssignmentId =>
        _httpContextAccessor.HttpContext.GetClaim<Guid?>(CustomClaimTypes.OriginAssignmentId);

    public Guid? SessionId => _httpContextAccessor.HttpContext.GetClaim<Guid?>(JwtRegisteredClaimNames.Sid);

    public IEnumerable<string> Privileges =>
        _httpContextAccessor.HttpContext.User.Claims
            .Where(c => c.Type == CustomClaimTypes.Privilege)
            .Select(c => c.Value)
            .ToArray();

    public bool IsInitialized => _httpContextAccessor?.HttpContext != null && UserId.HasValue;
}