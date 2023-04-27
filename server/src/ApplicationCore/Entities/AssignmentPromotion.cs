namespace ApplicationCore.Entities;

public class AssignmentPromotion : Entity
{
    public Guid AssignmentId { get; set; }

    public Assignment Assignment { get; set; }

    public Guid PromotionId { get; set; }

    public Promotion Promotion { get; set; }
}