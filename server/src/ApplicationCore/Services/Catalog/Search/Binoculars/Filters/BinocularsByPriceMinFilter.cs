using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Binoculars.Filters;

[ScopedDependency]
internal class BinocularsByPriceMinFilter : IEntityFilter<Binocular, BinocularSearchModel>
{
    public Expression<Func<Binocular, bool>> GetFilterPredicate(BinocularSearchModel model)
    {
        return model.PriceMin.HasValue
            ? b => b.Product.Price >= model.PriceMin
            : null;
    }
}