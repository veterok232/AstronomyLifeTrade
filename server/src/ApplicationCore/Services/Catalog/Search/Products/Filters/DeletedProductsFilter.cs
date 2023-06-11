using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Catalog.Search.Products.Filters;

[ScopedDependency]
internal class DeletedProductsFilter : IEntityFilter<Product, ProductsSearchModel>
{
    public Expression<Func<Product, bool>> GetFilterPredicate(ProductsSearchModel model)
    {
        return p => !p.DeletedAt.HasValue;
    }
}