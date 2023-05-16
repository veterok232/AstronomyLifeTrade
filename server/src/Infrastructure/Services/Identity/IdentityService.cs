using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Identity;
using ApplicationCore.Models;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Models.Identity.Login;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Utils;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Identity;

[ScopedDependency]
internal class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly ISessionSecurityService _sessionSecurityService;
    private readonly ISessionManager _sessionManager;
    private readonly IRepository<Session> _sessionRepository;
    private readonly IAccessTokenService _accessTokenService;
    private readonly IAuthContextAccessor _authContextAccessor;

    public IdentityService(
        UserManager<User> userManager,
        ISessionSecurityService sessionSecurityService,
        ISessionManager sessionManager,
        IAccessTokenService accessTokenService,
        IAuthContextAccessor authContextAccessor,
        IRepository<Session> sessionRepository)
    {
        _userManager = userManager;
        _sessionSecurityService = sessionSecurityService;
        _sessionManager = sessionManager;
        _accessTokenService = accessTokenService;
        _authContextAccessor = authContextAccessor;
        _sessionRepository = sessionRepository;
    }

    public async Task<LoginResult> Login(LoginData loginData)
    {
        var user = await _userManager.FindByEmailAsync(loginData.Email);
        var error = await ValidateLoginData(user, loginData);
        if (error != null)
        {
            return LoginResult.CreateFailed(user, error);
        }

        var session = await _sessionManager.CreateSession(user, loginData.Fingerprint);
        var token = await _accessTokenService.CreateToken(session);

        return LoginResult.CreateSucceeded(user, new IdentityData
        {
            Token = token,
            RefreshToken = session.RefreshToken,
            SessionExpirationDate = session.ExpiryDate,
            UserId = session.UserId,
        });
    }

    private async Task<string> ValidateLoginData(User user, LoginData loginData)
    {
        if (user == null)
        {
            return "Incorrect email or password!";
        }

        bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginData.Password);
        if (!isValidPassword)
        {
            return "Incorrect email or password!";
        }

        if (user.Assignment.Status == AssignmentStatus.Inactive)
        {
            return "Inactive account";
        }

        return null;
    }

    public async Task Logout()
    {
        var sessionId = _authContextAccessor.SessionId;
        if (sessionId != null)
        {
            var session = await _sessionRepository.GetById(sessionId.Value);
            await _sessionManager.InvalidateSession(session);
        }
    }

    public async Task<Result<RefreshAccessTokenData>> RefreshAccessToken(
        Guid refreshToken, ClientIdentificationData clientIdentificationData)
    {
        var validationResult =
            await _sessionSecurityService.ValidateSession(refreshToken, clientIdentificationData);

        if (!validationResult.IsValid)
        {
            return ResultBuilder.BuildFailed<RefreshAccessTokenData>();
        }

        var session = validationResult.Session;

        if (session.Assignment != null && session.Assignment.Status != AssignmentStatus.Active)
        {
            return ResultBuilder.BuildFailedWithData(new RefreshAccessTokenData
            {
                IsAssignmentInactive = true,
            });
        }

        session = await _sessionManager.UpdateRefreshToken(session);
        var token = await _accessTokenService.CreateToken(session);

        return ResultBuilder.BuildSucceeded(new RefreshAccessTokenData
        {
            Token = token,
            RefreshToken = session.RefreshToken,
            SessionExpirationDate = session.ExpiryDate,
        });
    }

    public async Task<Result<ExtendSessionResult>> ExtendSession(
        Guid refreshToken, ClientIdentificationData clientIdentificationData)
    {
        var validationResult =
            await _sessionSecurityService.ValidateSession(refreshToken, clientIdentificationData);

        if (!validationResult.IsValid)
        {
            return ResultBuilder.BuildFailed<ExtendSessionResult>();
        }

        var session = validationResult.Session;
        await _sessionManager.ExtendSession(session);

        return ResultBuilder.BuildSucceeded(new ExtendSessionResult
        {
            RefreshToken = session.RefreshToken,
            ExpiryDateTime = session.ExpiryDate,
        });
    }
}