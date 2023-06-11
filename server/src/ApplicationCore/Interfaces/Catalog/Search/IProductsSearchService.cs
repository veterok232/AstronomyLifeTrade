using ApplicationCore.Models.Catalog;

namespace ApplicationCore.Interfaces.Catalog.Search;

public interface IProductsSearchService
{
    Task<ICollection<ProductListItem>> GetPopularProducts();
}