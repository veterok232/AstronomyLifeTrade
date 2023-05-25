using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Users;

public interface IPasswordHasher
{
    string GetHash(User user, string password);

    bool IsPasswordMatch(User user, string hashedPassword, string providedPassword);
}