using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Catalog;

namespace ApplicationCore.Services.Catalog.Search.Products;

[ScopedDependency]
public class ProductsSearchService : IProductsSearchService
{
    private readonly IRepository<Product> _productsRepository;
    private readonly IFillCharacteristicsService _fillCharacteristicsService;
    
    public ProductsSearchService(
        IRepository<Product> productsRepository,
        IFillCharacteristicsService fillCharacteristicsService)
    {
        _productsRepository = productsRepository;
        _fillCharacteristicsService = fillCharacteristicsService;
    }

    public async Task<ICollection<ProductListItem>> GetPopularProducts()
    {
        var products = (await _productsRepository.List(
            new PopularProductsSpecification())).ToList();

        await _fillCharacteristicsService.FillCharacteristics(products);

        return products;
    }
}