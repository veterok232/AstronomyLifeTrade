namespace ApplicationCore.Interfaces.Catalog.ProductDetails;

public interface IProductDetailsService
{
    Task<Models.Catalog.ProductDetails> Get(Guid productId);
}