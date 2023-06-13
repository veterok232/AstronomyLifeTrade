using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Accessories.Filters;

[ScopedDependency]
internal class AccessoriesByBrandsFilter : IEntityFilter<Accessory, AccessoriesSearchModel>
{
    public Expression<Func<Accessory, bool>> GetFilterPredicate(AccessoriesSearchModel model)
    {
        return model.BrandsIds.IsNullOrEmpty()
            ? null
            : a => model.BrandsIds.Contains(a.Product.BrandId);
    }
}