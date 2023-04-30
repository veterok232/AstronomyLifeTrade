using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class SetupPasswordHistory : Entity, IHasCreatedAt
{
    public Guid UserId { get; set; }

    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; }
}