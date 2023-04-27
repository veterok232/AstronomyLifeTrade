using System.ComponentModel;
using System.Security.Claims;
using Api.Constants;

namespace Api.Extensions;

internal static class HttpContextExtensions
{
    public static Guid? GetUserId(this HttpContext httpContext) =>
        httpContext.GetClaim<Guid?>(CustomClaimTypes.UserId);

    public static Guid? GetAssignmentId(this HttpContext httpContext) =>
        httpContext.GetClaim<Guid?>(CustomClaimTypes.AssignmentId);

    public static ClaimsIdentity GetClaimsIdentity(this HttpContext httpContext) =>
        httpContext.User?.Identities?.First();

    public static bool IsCurrentAssignmentChosen(this HttpContext httpContext) =>
        httpContext.GetClaimsIdentity().Claims.Any(c => c.Type == CustomClaimTypes.AssignmentId);

    public static T GetClaim<T>(this HttpContext httpContext, string claimType)
    {
        if (httpContext.User == null)
        {
            return default;
        }

        string claimValue = httpContext.User.Claims.SingleOrDefault(x => x.Type == claimType)?.Value;
        return !string.IsNullOrEmpty(claimValue)
            ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claimValue)
            : default;
    }
}