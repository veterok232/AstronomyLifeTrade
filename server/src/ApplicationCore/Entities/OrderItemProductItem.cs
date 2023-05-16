namespace ApplicationCore.Entities;

public class OrderItemProductItem : Entity
{
    public Guid OrderItemId { get; set; }
    
    public OrderItem OrderItem { get; set; }
    
    public Guid ProductItemId { get; set; }
    
    public ProductItem ProductItem { get; set; }
}