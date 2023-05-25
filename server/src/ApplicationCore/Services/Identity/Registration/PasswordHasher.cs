using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Users;
using ApplicationCore.Services.Dependencies.Attributes;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Services.Identity.Registration;

[ScopedDependency]
public class PasswordHasher : IPasswordHasher
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordHasher(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string GetHash(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool IsPasswordMatch(User user, string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword) ==
               PasswordVerificationResult.Success;
    }
}