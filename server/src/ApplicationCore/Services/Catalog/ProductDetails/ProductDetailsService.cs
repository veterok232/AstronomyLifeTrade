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
    private readonly IRepository<Binocular> _binocularsRepository;
    private readonly IRepository<Accessory> _accessoriesRepository;

    public ProductDetailsService(
        IRepository<Product> productRepository,
        IRepository<Telescope> telescopesRepository,
        IRepository<Binocular> binocularsRepository,
        IRepository<Accessory> accessoriesRepository)
    {
        _productRepository = productRepository;
        _telescopesRepository = telescopesRepository;
        _binocularsRepository = binocularsRepository;
        _accessoriesRepository = accessoriesRepository;
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
            case "2":
                return await _binocularsRepository.GetSingleOrDefault(
                    new BinocularDetailsByProductIdSpecification(productId));
            case "3":
                return await _accessoriesRepository.GetSingleOrDefault(
                    new AccessoryDetailsByProductIdSpecification(productId));
            default: return null;
        }
    }
}