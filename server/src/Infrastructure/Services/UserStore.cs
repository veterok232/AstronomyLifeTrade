using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

[ScopedDependency]
public sealed class UserStore : IUserPasswordStore<User>, IUserEmailStore<User>
{
    private readonly IRepository<User> _userRepository;

    public UserStore(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    // IUserStore implementation
    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) =>
        Task.FromResult(user.Id.ToString());

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        // used email property because userName doesn't exist in our infrastructure
        return Task.FromResult(user.Email);
    }

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(SetUserNameAsync));

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) =>
        Task.FromResult((object)null);

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _userRepository.Add(user);

        return await Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(UpdateAsync));

    public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(UpdateAsync));

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            return await _userRepository.GetSingleOrDefault(new UserByIdSpecification(Guid.Parse(userId)));
        }

        return await Task.FromResult((User)null);
    }

    public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        // used email property because userName doesn't exist in our infrastructure
        return await _userRepository.GetSingleOrDefault(new ActiveUserByEmailForLoginSpecification(normalizedUserName));
    }

    // IUserPasswordStore implementation
    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.FromResult((object)null);
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) =>
        Task.FromResult(user.PasswordHash);

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) =>
        Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));

    // IUserEmailStore implementation
    public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        return await _userRepository.GetSingleOrDefault(new ActiveUserByEmailForLoginSpecification(normalizedEmail));
    }

    public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken) =>
        Task.FromResult(user.Email);

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(GetEmailConfirmedAsync));

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(GetEmailConfirmedAsync));

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(SetEmailAsync));

    public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken) =>
        throw new NotImplementedException(nameof(SetEmailConfirmedAsync));

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken) =>
        Task.FromResult((object)null);

    public void Dispose()
    {
        // nothing to dispose
    }
}