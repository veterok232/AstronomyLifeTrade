namespace ApplicationCore.Entities;

public class Session : Entity
{
    public DateTime CreationDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool Invalidated { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid? AssignmentId { get; set; }

    public Assignment Assignment { get; set; }

    public Guid? OriginAssignmentId { get; set; }

    public string Fingerprint { get; set; }

    public Guid RefreshToken { get; set; }
}