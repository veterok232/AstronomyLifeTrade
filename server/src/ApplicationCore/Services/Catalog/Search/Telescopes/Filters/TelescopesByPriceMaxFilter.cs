using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Telescopes.Filters;

[ScopedDependency]
internal class TelescopesByPriceMaxFilter : IEntityFilter<Telescope, TelescopeSearchModel>
{
    public Expression<Func<Telescope, bool>> GetFilterPredicate(TelescopeSearchModel model)
    {
        return model.PriceMax.HasValue
            ? t => t.Product.Price <= model.PriceMax
            : null;
    }
}