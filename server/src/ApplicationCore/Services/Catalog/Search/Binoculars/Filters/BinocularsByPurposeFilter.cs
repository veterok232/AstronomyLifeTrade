using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Binoculars.Filters;

[ScopedDependency]
internal class BinocularsByPurposeFilter : IEntityFilter<Binocular, BinocularSearchModel>
{
    public Expression<Func<Binocular, bool>> GetFilterPredicate(BinocularSearchModel model)
    {
        return model.BinocularPurposes.IsNullOrEmpty()
            ? _ => false
            : b => model.BinocularPurposes.Contains(b.Purpose);
    }
}