using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Order : Entity, IHasCreatedAt, IHasModifiedAt
{
    public Guid AssignmentId { get; set; }
    
    public Assignment Assignment { get; set; }
    
    public OrderStatus OrderStatus { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public Guid ManagerAssignmentId { get; set; }
    
    public Assignment ManagerAssignment { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}