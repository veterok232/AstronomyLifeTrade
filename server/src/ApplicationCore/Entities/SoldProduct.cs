using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class SoldProduct : Entity, IHasCreatedAt
{
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public DateTime CreatedAt { get; set; }
}