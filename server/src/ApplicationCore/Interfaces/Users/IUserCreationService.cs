using ApplicationCore.Entities;
using ApplicationCore.Models.Identity;

namespace ApplicationCore.Interfaces.Users;

public interface IUserCreationService
{
    Task<Assignment> Create(UserRegistrationModel model);
}