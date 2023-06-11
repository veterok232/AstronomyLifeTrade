using ApplicationCore.Models.Catalog;

namespace ApplicationCore.Interfaces.Catalog;

public interface ICatalogService
{
    Task<ICollection<BrandModel>> GetBrands();

    Task<ICollection<CategoryModel>> GetCategories();
}