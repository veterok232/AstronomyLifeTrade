using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Payment : Entity, IHasCreatedAt, IHasModifiedAt
{
    public PaymentStatus PaymentStatus { get; set; }
    
    public decimal Amount { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
    
    public PaymentMethod PaymentType { get; set; }
    
    public Guid ConsumerAssignmentId { get; set; }
    
    public Assignment ConsumerAssingment { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}