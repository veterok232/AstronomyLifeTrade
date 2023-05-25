using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Telescopes.Filters;

[ScopedDependency]
internal class TelescopesByUserLevelFilter : IEntityFilter<Telescope, TelescopeSearchModel>
{
    public Expression<Func<Telescope, bool>> GetFilterPredicate(TelescopeSearchModel model)
    {
        return model.UserLevels.IsNullOrEmpty()
            ? null
            : t => model.UserLevels.Contains(t.TelescopeUserLevel.Value);
    }
}