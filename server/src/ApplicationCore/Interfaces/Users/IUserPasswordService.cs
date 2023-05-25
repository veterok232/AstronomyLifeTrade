using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Users;

public interface IUserPasswordService
{
    void AssignUserPassword(User user, string password);
}