using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Identity;
using ApplicationCore.Interfaces.Identity.Login;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity.Login;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Identity.Login;

[ScopedDependency]
internal class LoginService : ILoginService
{
    private readonly IIdentityService _identityService;
    private readonly ILoginResultHandlingService _loginResultHandlingService;
    private readonly IAppLogger<LoginService> _logger;

    public LoginService(
        IIdentityService identityService,
        ILoginResultHandlingService loginResultHandlingService,
        IAppLogger<LoginService> logger)
    {
        _identityService = identityService;
        _loginResultHandlingService = loginResultHandlingService;
        _logger = logger;
    }

    public async Task<Result> Login(LoginData data)
    {
        _logger.Info($"User '{data.Email}' log in request.");

        var loginResult = await _identityService.Login(data);
        var result = await _loginResultHandlingService.Handle(loginResult);

        LogResponse(data.Email, result);

        return result;
    }

    private void LogResponse(string userName, Result result)
    {
        string logResultMessage = result.IsSucceeded ? "Succeeded" : "Failed";
        _logger.Info($"User '{userName}' log in result: {logResultMessage}.");
    }
}