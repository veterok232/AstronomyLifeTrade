using System.Net.Mime;
using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Assignment : Entity, IHasVersion, IHasCreatedByUser, IHasCreatedAt, IHasUpdatedAt
{
    public Guid Version { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    /// <summary>
    ///     Gets or sets official User ID which can be used e.g. for mapping assignment to user in other external Systems.
    /// </summary>
    public string AffiliateNumber { get; set; }

    public AssignmentStatus Status { get; set; }

    public string Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedByUserId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid RoleId { get; set; }

    public Role Role { get; set; }

    public ICollection<AssignmentPermission> AssignmentPermissions { get; set; }

    public ICollection<AssignmentPromotion> Promotions { get; set; }
}