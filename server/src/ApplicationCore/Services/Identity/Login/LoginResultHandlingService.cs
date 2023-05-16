using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity.Login;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Utils;

namespace ApplicationCore.Services.Identity.Login;

[ScopedDependency]
internal class LoginResultHandlingService : ILoginResultHandlingService
{
    private readonly IRepository<User> _userRepository;

    public LoginResultHandlingService(
        IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(LoginResult result)
    {
        if (result.User == null)
        {
            return ResultBuilder.BuildFailed(result.Error);
        }

        /*if (IsLocked(result.User))
        {
            return BuildAccountBlockedResult();
        }*/

        if (result.IsSucceeded)
        {
            return ResultBuilder.BuildSucceeded(result.IdentityData);
        }

        /*if (result.Error == LoginErrors.IncorrectEmailOrPassword)
        {
            var user = await _wrongPasswordLogInAttemptsTracker.RecordAttempt(result.User);

            if (IsLocked(user))
            {
                var tokenCreationResult = await _passwordTokenService.CreateTokenByEmail(user.Email);

                await _userBlockedEmailSender.Send(user, tokenCreationResult.Data.TokenValue);

                return BuildAccountBlockedResult();
            }
        }*/

        return ResultBuilder.BuildFailed(result.Error);
    }

    /*private static bool IsLocked(User user)
    {
        return user.LockedAt > DateTime.UtcNow.AddMinutes(-LogInConstants.UserLockedDurationInMinutes);
    }*/

    /*private async Task ResetWrongPasswordAttemptsCount(User user)
    {
        if (user.WrongPasswordLogInAttemptsCount != 0)
        {
            user.WrongPasswordLogInAttemptsCount = 0;
            await _userRepository.Update(user);
        }
    }*/

    /*private static Result BuildAccountBlockedResult()
    {
        return ResultBuilder.BuildFailed(LoginErrors.AccountIsBlocked);
    }*/
}