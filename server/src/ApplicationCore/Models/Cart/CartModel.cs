namespace ApplicationCore.Models.Cart;

public class CartModel
{
    public ICollection<CartItemModel> CartItems { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public int Quantity { get; set; }
}