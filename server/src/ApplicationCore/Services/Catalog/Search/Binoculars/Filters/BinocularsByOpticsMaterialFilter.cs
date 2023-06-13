using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Binoculars.Filters;

[ScopedDependency]
internal class BinocularsByOpticsMaterialFilter : IEntityFilter<Binocular, BinocularSearchModel>
{
    public Expression<Func<Binocular, bool>> GetFilterPredicate(BinocularSearchModel model)
    {
        return model.OpticsMaterials.IsNullOrEmpty()
            ? null
            : b => model.OpticsMaterials.Contains(b.OpticsMaterial);
    }
}