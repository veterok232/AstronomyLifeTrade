using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class SetupPasswordToken : Entity, IHasCreatedAt
{
    public Guid UserId { get; set; }

    public string TokenValue { get; set; }

    public DateTime ExpiryDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsUsed { get; set; }
}