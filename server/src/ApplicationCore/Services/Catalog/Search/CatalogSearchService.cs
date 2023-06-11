using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.AstronomicalCalculator;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.AstronomicalCalculator;
using ApplicationCore.Specifications.Catalog;
using AutoMapper;

namespace ApplicationCore.Services.Catalog.Search;

[ScopedDependency]
internal class CatalogSearchService : ICatalogSearchService
{
    private readonly IRepository<SoldProduct> _soldProductRepository;
    private readonly IRepository<Telescope> _telescopesRepository;
    private readonly IRepository<Binocular> _binocularsRepository;
    private readonly IRepository<Product> _productsRepository;
    private readonly IRepository<Accessory> _accessoriesRepository;
    private readonly IMapper _mapper;
    private readonly IEntityFilterQueryBuilder<Telescope, TelescopeSearchModel> _telescopesFilterQueryBuilder;
    private readonly IEntityFilterQueryBuilder<Binocular, BinocularSearchModel> _binocularsFilterQueryBuilder;
    private readonly IEntityFilterQueryBuilder<Product, ProductsSearchModel> _productsFilterQueryBuilder;
    private readonly IEntityFilterQueryBuilder<Accessory, AccessoriesSearchModel> _accessoriesFilterQueryBuilder;
    private readonly IFillCharacteristicsService _fillCharacteristicsService;
    
    public CatalogSearchService(
        IRepository<SoldProduct> soldProductRepository,
        IMapper mapper,
        IRepository<Telescope> telescopesRepository,
        IRepository<Binocular> binocularsRepository,
        IEntityFilterQueryBuilder<Telescope, TelescopeSearchModel> telescopesFilterQueryBuilder,
        IEntityFilterQueryBuilder<Binocular, BinocularSearchModel> binocularsFilterQueryBuilder,
        IEntityFilterQueryBuilder<Product, ProductsSearchModel> productsFilterQueryBuilder,
        IRepository<Product> productsRepository,
        IFillCharacteristicsService fillCharacteristicsService,
        IRepository<Accessory> accessoriesRepository,
        IEntityFilterQueryBuilder<Accessory, AccessoriesSearchModel> accessoriesFilterQueryBuilder)
    {
        _soldProductRepository = soldProductRepository;
        _mapper = mapper;
        _telescopesRepository = telescopesRepository;
        _binocularsRepository = binocularsRepository;
        _telescopesFilterQueryBuilder = telescopesFilterQueryBuilder;
        _binocularsFilterQueryBuilder = binocularsFilterQueryBuilder;
        _productsFilterQueryBuilder = productsFilterQueryBuilder;
        _productsRepository = productsRepository;
        _fillCharacteristicsService = fillCharacteristicsService;
        _accessoriesRepository = accessoriesRepository;
        _accessoriesFilterQueryBuilder = accessoriesFilterQueryBuilder;
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
        
        var result = _mapper.Map<SearchResult<ProductListItem>>(list);
        
        return result;
    }

    public async Task<SearchResult<ProductListItem>> GetBinoculars(BinocularSearchModel model)
    {
        var list = await _binocularsRepository.Search(
            new BinocularsListSpecification(
                GetBinocularsSearchData(model)));
        
        var result = _mapper.Map<SearchResult<ProductListItem>>(list);
        
        return result;
    }

    public async Task<SearchResult<ProductListItem>> GetAccessories(AccessoriesSearchModel model)
    {
        var list = await _accessoriesRepository.Search(
            new AccessoriesListSpecification(
                GetAccessoriesSearchData(model)));
        
        var result = _mapper.Map<SearchResult<ProductListItem>>(list);
        
        return result;
    }

    public async Task<SearchResult<ProductListItem>> SearchProducts(ProductsSearchModel model)
    {
        var list = await _productsRepository.Search(
            new SearchProductsListSpecification(
                GetProductsSearchData(model)));

        await _fillCharacteristicsService.FillCharacteristics(list.Items.ToList());
        
        var result = _mapper.Map<SearchResult<ProductListItem>>(list);
        
        return result;
    }

    public async Task<ICollection<ProductListItem>> GetTelescopesForCalculator(
        AstronomicalCalculatorMostMatchingModel model)
    {
        return (await _telescopesRepository.List(
            new TelescopesForCalculatorSpecification(model))).ToList();
    }

    private TelescopesSearchData GetTelescopesSearchData(TelescopeSearchModel model)
    {
        var data = _mapper.Map<TelescopesSearchData>(model);
        data.FilterPredicate = _telescopesFilterQueryBuilder.BuildQuery(model);

        return data;
    }

    private BinocularsSearchData GetBinocularsSearchData(BinocularSearchModel model)
    {
        var data = _mapper.Map<BinocularsSearchData>(model);
        data.FilterPredicate = _binocularsFilterQueryBuilder.BuildQuery(model);

        return data;
    }
    
    private ProductsSearchData GetProductsSearchData(ProductsSearchModel model)
    {
        var data = _mapper.Map<ProductsSearchData>(model);
        data.FilterPredicate = _productsFilterQueryBuilder.BuildQuery(model);

        return data;
    }
    
    private AccessoriesSearchData GetAccessoriesSearchData(AccessoriesSearchModel model)
    {
        var data = _mapper.Map<AccessoriesSearchData>(model);
        data.FilterPredicate = _accessoriesFilterQueryBuilder.BuildQuery(model);

        return data;
    }
}