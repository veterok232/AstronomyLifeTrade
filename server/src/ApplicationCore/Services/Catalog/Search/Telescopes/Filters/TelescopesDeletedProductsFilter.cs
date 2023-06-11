using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Telescopes.Filters;

[ScopedDependency]
internal class TelescopesDeletedProductsFilter : IEntityFilter<Telescope, TelescopeSearchModel>
{
    public Expression<Func<Telescope, bool>> GetFilterPredicate(TelescopeSearchModel model)
    {
        return t => !t.Product.DeletedAt.HasValue;
    }
}