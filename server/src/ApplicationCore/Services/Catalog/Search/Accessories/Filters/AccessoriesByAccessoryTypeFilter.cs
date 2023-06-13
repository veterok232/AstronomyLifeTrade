using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Accessories.Filters;

[ScopedDependency]
internal class AccessoriesByAccessoryTypeFilter : IEntityFilter<Accessory, AccessoriesSearchModel>
{
    public Expression<Func<Accessory, bool>> GetFilterPredicate(AccessoriesSearchModel model)
    {
        return model.AccessoryTypes.IsNullOrEmpty()
            ? _ => false
            : a => model.AccessoryTypes.Contains(a.AccessoryType);
    }
}