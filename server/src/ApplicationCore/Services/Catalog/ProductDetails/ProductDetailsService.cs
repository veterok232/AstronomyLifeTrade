using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.ProductDetails;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Catalog;

namespace ApplicationCore.Services.Catalog.ProductDetails;

[ScopedDependency]
public class ProductDetailsService : IProductDetailsService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Telescope> _telescopesRepository;

    public ProductDetailsService(
        IRepository<Product> productRepository,
        IRepository<Telescope> telescopesRepository)
    {
        _productRepository = productRepository;
        _telescopesRepository = telescopesRepository;
    }

    public async Task<Models.Catalog.ProductDetails> Get(Guid productId)
    {
        var product = await _productRepository.GetSingleOrDefault(
            new ProductByIdForDetailsSpecification(productId));

        switch (product.Category.Code)
        {
            case "1":
                return await _telescopesRepository.GetSingleOrDefault(
                    new TelescopeDetailsByProductIdSpecification(productId));
            default: return null;
        }
    }
}