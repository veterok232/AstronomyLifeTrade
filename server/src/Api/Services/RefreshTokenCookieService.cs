using Api.Constants;
using Api.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;

namespace Api.Services;

[ScopedDependency]
internal class RefreshTokenCookieService : IRefreshTokenCookieService
{
    public void SetCookie(HttpContext httpContext, Guid refreshToken, DateTime expirationDate)
    {
        httpContext.Response.Cookies.Append(
            CookieNames.RefreshToken,
            refreshToken.ToString(),
            GetRefreshTokenCookieOptions(expirationDate: new DateTimeOffset(
                DateTime.SpecifyKind(expirationDate, DateTimeKind.Utc), TimeSpan.Zero)));
    }

    public Guid? GetCookie(HttpContext httpContext)
    {
        var refreshToken = httpContext.Request.Cookies[CookieNames.RefreshToken];

        return string.IsNullOrEmpty(refreshToken) ? (Guid?)null : Guid.Parse(refreshToken);
    }

    public void DeleteCookie(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Delete(CookieNames.RefreshToken, GetRefreshTokenCookieOptions());
    }

    private static CookieOptions GetRefreshTokenCookieOptions(DateTimeOffset? expirationDate = null)
    {
        return new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            IsEssential = true,
            Expires = expirationDate,
            SameSite = SameSiteMode.None,
            Path = $"{Routes.Api}/{Routes.Identity.Root}",
        };
    }
}