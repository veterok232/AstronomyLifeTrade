using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Accessories.Filters;

[ScopedDependency]
internal class AccessoriesDeletedProductsFilter : IEntityFilter<Accessory, AccessoriesSearchModel>
{
    public Expression<Func<Accessory, bool>> GetFilterPredicate(AccessoriesSearchModel model)
    {
        return a => !a.Product.DeletedAt.HasValue;
    }
}