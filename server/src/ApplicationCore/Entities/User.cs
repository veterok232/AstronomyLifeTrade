using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class User : Entity, IHasCreatedAt, IHasUpdatedAt
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    /// <summary>
    ///     Gets or sets the last password change date.
    /// </summary>
    public DateTime? PasswordChangedAt { get; set; }

    public DateTime? FirstLoginDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Assignment Assignment { get; set; }

    public int WrongPasswordLogInAttemptsCount { get; set; }

    public DateTime? LockedAt { get; set; }
}