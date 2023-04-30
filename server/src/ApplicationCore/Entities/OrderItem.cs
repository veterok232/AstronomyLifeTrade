using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class OrderItem : Entity, IHasCreatedAt, IHasModifiedAt
{
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
    
    public Guid ProductItemId { get; set; }
    
    public ProductItem ProductItem { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}