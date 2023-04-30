using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Cart : Entity, IHasCreatedAt, IHasModifiedAt
{
    public Guid CustomerAssignmentId { get; set; }
    
    public Assignment Assignment { get; set; }
    
    public DateTime CreatedAt { get; set; }
 
    public DateTime ModifiedAt { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public int Quantity { get; set; }
}