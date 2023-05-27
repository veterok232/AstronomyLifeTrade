using ApplicationCore.Models.Catalog;

namespace ApplicationCore.Models.Cart;

public class CartItemModel
{
    public Guid Id { get; set; }
    
    public ProductListItem Product { get; set; }
    
    public int Quantity { get; set; }
}