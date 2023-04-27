using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class User : Entity, IHasCreatedByUser, IHasCreatedAt, IHasUpdatedAt
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly Birthday { get; set; }

    /// <summary>
    ///     Gets or sets the last password change date.
    /// </summary>
    public DateTime? PasswordChangedAt { get; set; }

    public DateTime? FirstLoginDate { get; set; }

    public Guid CreatedByUserId { get; set; }

    public User CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? LastChosenAssignmentId { get; set; }

    public ICollection<Assignment> Assignments { get; set; }

    public int WrongPasswordLogInAttemptsCount { get; set; }

    public DateTime? LockedAt { get; set; }

    /// <summary>
    ///     Gets assignmentId if it is single and active, otherwise null.
    /// </summary>
    public Guid? SoleActiveAssignmentId
    {
        get
        {
            IEnumerable<Assignment> activeAssignments = Assignments?.Where(a => a.Status == AssignmentStatus.Active);
            if (activeAssignments?.Count() == 1)
            {
                return activeAssignments.Single().Id;
            }

            return null;
        }
    }
}