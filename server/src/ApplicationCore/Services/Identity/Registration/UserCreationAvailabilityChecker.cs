using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Users;
using ApplicationCore.Models.Common;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Users;
using ApplicationCore.Utils;

namespace ApplicationCore.Services.Identity.Registration;

[ScopedDependency]
internal class UserCreationAvailabilityChecker : IUserCreationAvailabilityChecker
{
    private readonly IRepository<User> _userRepository;

    public UserCreationAvailabilityChecker(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Check(string email)
    {
        var users = await _userRepository.List(new UserForDuplicatesCheckSpecification(email));

        return users.Count == 0
            ? ResultBuilder.BuildSucceeded()
            : ResultBuilder.BuildFailed("EmailErrors.AlreadyExists");
    }
}