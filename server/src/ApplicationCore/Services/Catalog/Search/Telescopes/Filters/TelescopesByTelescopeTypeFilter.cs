using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Telescopes.Filters;

[ScopedDependency]
internal class TelescopesByTelescopeTypeFilter : IEntityFilter<Telescope, TelescopeSearchModel>
{
    public Expression<Func<Telescope, bool>> GetFilterPredicate(TelescopeSearchModel model)
    {
        return model.TelescopeTypes.IsNullOrEmpty()
            ? _ => false
            : t => model.TelescopeTypes.Contains(t.Type.Value);
    }
}