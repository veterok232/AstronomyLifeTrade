using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Users;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Identity.Registration;

[ScopedDependency]
public class UserPasswordService : IUserPasswordService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRepository<SetupPasswordHistory> _setupPasswordHistoryRepository;
    private readonly IRepository<User> _userRepository;

    public UserPasswordService(
        IPasswordHasher passwordHasher,
        IRepository<SetupPasswordHistory> setupPasswordHistoryRepository,
        IRepository<User> userRepository)
    {
        _passwordHasher = passwordHasher;
        _setupPasswordHistoryRepository = setupPasswordHistoryRepository;
        _userRepository = userRepository;
    }
    
    public void AssignUserPassword(User user, string password)
    {
        string passwordHash = _passwordHasher.GetHash(user, password);

        user.PasswordHash = passwordHash;
        user.PasswordChangedAt = DateTime.UtcNow;

        /*await SavePasswordHistory(user.Id, passwordHash);*/
    }
    
    /*private Task SavePasswordHistory(Guid userId, string passwordHash) =>
        _setupPasswordHistoryRepository.Add(new SetupPasswordHistory
        {
            PasswordHash = passwordHash,
            UserId = userId,
        });*/
}