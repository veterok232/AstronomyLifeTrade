using ApplicationCore.Entities;
using ApplicationCore.Extensions;

namespace ApplicationCore.Models.Identity.Login;

public class LoginResult
{
    public bool IsSucceeded => Error.IsNullOrEmpty();

    public string Error { get; set; }

    public User User { get; set; }

    public IdentityData IdentityData { get; set; }

    public static LoginResult CreateFailed(User user, string error)
    {
        return new LoginResult { Error = error, User = user };
    }

    public static LoginResult CreateSucceeded(User user, IdentityData identityData)
    {
        return new LoginResult { IdentityData = identityData, User = user };
    }
}