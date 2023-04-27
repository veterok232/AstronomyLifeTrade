namespace Api.Interfaces;

public interface IRefreshTokenCookieService
{
    public void SetCookie(HttpContext httpContext, Guid refreshToken, DateTime expirationDate);

    public Guid? GetCookie(HttpContext httpContext);

    public void DeleteCookie(HttpContext httpContext);
}