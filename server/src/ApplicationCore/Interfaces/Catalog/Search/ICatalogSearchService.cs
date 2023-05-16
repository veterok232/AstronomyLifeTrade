using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;

namespace ApplicationCore.Interfaces.Catalog.Search;

public interface ICatalogSearchService
{
    Task<ICollection<ProductListItem>> GetPopularProducts();

    Task<SearchResult<ProductListItem>> GetTelescopes(TelescopeSearchModel model);
}