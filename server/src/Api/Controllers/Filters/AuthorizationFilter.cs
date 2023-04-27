using System.Security.Claims;
using Api.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Controllers.Filters;

/// <summary>
///     Authorization filter to check if user has specific role/permission in user claims.
/// </summary>
internal class AuthorizationFilter : IAuthorizationFilter
{
    private readonly string[] _privileges;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthorizationFilter"/> class.
    /// </summary>
    /// <param name="privileges">The permission or roles depending on context.</param>
    public AuthorizationFilter(params string[] privileges)
    {
        _privileges = privileges;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool hasAccess = CheckUserPermissionBasedOnRoleClaims(context.HttpContext.User.Claims);
        if (!hasAccess)
        {
            context.Result = new ForbidResult();
        }
    }

    private bool CheckUserPermissionBasedOnRoleClaims(IEnumerable<Claim> claims)
    {
        return claims.Any(c => c.Type == CustomClaimTypes.Privilege && _privileges.Any(p => p == c.Value));
    }
}
