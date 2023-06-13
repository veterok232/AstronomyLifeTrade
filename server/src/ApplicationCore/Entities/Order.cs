using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Order : Entity, IHasCreatedAt, IHasModifiedAt
{
    public Guid ConsumerAssignmentId { get; set; }
    
    public Assignment ConsumerAssignment { get; set; }
    
    public OrderStatus OrderStatus { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public DeliveryType DeliveryType { get; set; }
    
    public Guid? ManagerAssignmentId { get; set; }
    
    public Assignment? ManagerAssignment { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
    
    public int OrderNumber { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public Guid? AddressId { get; set; }
    
    public Address? Address { get; set; }
    
    public string? CustomerNotes { get; set; }
    
    public string? PromoCode { get; set; }
    
    public decimal? PromoRate { get; set; }
    
    public decimal? PromoAmount { get; set; }
}