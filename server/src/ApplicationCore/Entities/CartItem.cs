using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class CartItem : Entity, IHasCreatedAt
{
    public Guid CartId { get; set; }
    
    public Cart Cart { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int Quantity { get; set; }
}