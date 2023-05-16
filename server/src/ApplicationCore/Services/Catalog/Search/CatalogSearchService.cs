using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Catalog;
using AutoMapper;

namespace ApplicationCore.Services.Catalog.Search;

[ScopedDependency]
public class CatalogSearchService : ICatalogSearchService
{
    private readonly IRepository<SoldProduct> _soldProductRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CatalogSearchService(
        IRepository<SoldProduct> soldProductRepository,
        IMapper mapper,
        IRepository<Product> productRepository)
    {
        _soldProductRepository = soldProductRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ICollection<ProductListItem>> GetPopularProducts()
    {
        var soldProducts = (await _soldProductRepository.List(
            new ProductListItemForPopularProductsSpecification())).Select(sp => sp.Product);

        return _mapper.Map<ICollection<ProductListItem>>(soldProducts);
    }

    public async Task<ICollection<ProductListItem>> GetTelescopes()
    {
        var products = (await _soldProductRepository.List(
            new ProductListItemForPopularProductsSpecification())).Select(sp => sp.Product);

        return _mapper.Map<ICollection<ProductListItem>>(soldProducts);
    }
}