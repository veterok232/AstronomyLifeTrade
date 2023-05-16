using ApplicationCore.Models;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Models.Identity.Login;

namespace ApplicationCore.Interfaces.Identity;

public interface IIdentityService
{
    Task<LoginResult> Login(LoginData loginData);

    Task Logout();

    Task<Result<RefreshAccessTokenData>> RefreshAccessToken(
        Guid refreshToken, ClientIdentificationData clientIdentificationData);

    Task<Result<ExtendSessionResult>> ExtendSession(
        Guid refreshToken, ClientIdentificationData clientIdentificationData);
}