using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.AstronomicalCalculator;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;

namespace ApplicationCore.Interfaces.Catalog.Search;

public interface ICatalogSearchService
{
    Task<SearchResult<ProductListItem>> GetTelescopes(TelescopeSearchModel model);
    
    Task<SearchResult<ProductListItem>> GetBinoculars(BinocularSearchModel model);
    
    Task<SearchResult<ProductListItem>> GetAccessories(AccessoriesSearchModel model);
    
    Task<SearchResult<ProductListItem>> SearchProducts(ProductsSearchModel model);
    
    Task<ICollection<ProductListItem>> GetTelescopesForCalculator(AstronomicalCalculatorMostMatchingModel model);
}