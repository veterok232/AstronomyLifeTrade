using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Products.Filters;

[ScopedDependency]
internal class ProductBySearchStringFilter : IEntityFilter<Product, ProductsSearchModel>
{
    public Expression<Func<Product, bool>> GetFilterPredicate(ProductsSearchModel model)
    {
        return model.SearchString.IsNullOrEmpty()
            ? null
            : p => p.Name.ToLower().Contains(model.SearchString.ToLower()) ||
                   p.Code.ToLower().Contains(model.SearchString.ToLower());
    }
}