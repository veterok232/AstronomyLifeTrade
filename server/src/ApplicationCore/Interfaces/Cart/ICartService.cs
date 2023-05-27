using ApplicationCore.Models.Cart;

namespace ApplicationCore.Interfaces.Cart;

public interface ICartService
{
    Task<CartModel> Get();
    
    Task<ICollection<Guid>> GetProductsInCart();
    
    Task Add(Guid productId);
    
    Task Remove(Guid productId);

    Task ChangeProductCount(CartChangeCountModel model);
    
    Task Clear();
}