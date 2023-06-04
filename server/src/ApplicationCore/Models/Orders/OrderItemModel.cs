using ApplicationCore.Models.Catalog;

namespace ApplicationCore.Models.Orders;

public class OrderItemModel
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public ProductListItem Product { get; set; }
    
    public int Quantity { get; set; }
}