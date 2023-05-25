using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Catalog;
using AutoMapper;

namespace ApplicationCore.Services.Catalog.Search;

[ScopedDependency]
internal class CatalogSearchService : ICatalogSearchService
{
    private readonly IRepository<SoldProduct> _soldProductRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Telescope> _telescopesRepository;
    private readonly IRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;
    private readonly IEntityFilterQueryBuilder<Telescope, TelescopeSearchModel> _entityFilterQueryBuilder;

    public CatalogSearchService(
        IRepository<SoldProduct> soldProductRepository,
        IRepository<Comment> commentsRepository,
        IMapper mapper,
        IRepository<Product> productRepository,
        IRepository<Telescope> telescopesRepository,
        IEntityFilterQueryBuilder<Telescope, TelescopeSearchModel> entityFilterQueryBuilder)
    {
        _soldProductRepository = soldProductRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _telescopesRepository = telescopesRepository;
        _commentsRepository = commentsRepository;
        _entityFilterQueryBuilder = entityFilterQueryBuilder;
    }

    public async Task<ICollection<ProductListItem>> GetPopularProducts()
    {
        var soldProducts = (await _soldProductRepository.List(
            new ProductListItemForPopularProductsSpecification())).Select(sp => sp.Product);

        return _mapper.Map<ICollection<ProductListItem>>(soldProducts);
    }

    public async Task<SearchResult<ProductListItem>> GetTelescopes(TelescopeSearchModel searchModel)
    {
        var list = await _telescopesRepository.Search(
            new TelescopesListSpecification(
                GetTelescopesSearchData(searchModel)));

        return _mapper.Map<SearchResult<ProductListItem>>(list);
    }
    
    private TelescopesSearchData GetTelescopesSearchData(TelescopeSearchModel model)
    {
        var data = _mapper.Map<TelescopesSearchData>(model);
        data.FilterPredicate = _entityFilterQueryBuilder.BuildQuery(model);

        return data;
    }
}