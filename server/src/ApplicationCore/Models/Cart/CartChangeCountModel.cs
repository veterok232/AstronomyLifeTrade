namespace ApplicationCore.Models.Cart;

public class CartChangeCountModel
{
    public int Count { get; set; }
    
    public Guid ProductId { get; set; }
}