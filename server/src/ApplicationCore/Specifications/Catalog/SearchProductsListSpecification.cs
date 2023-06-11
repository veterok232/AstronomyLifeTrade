using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog.Search;

namespace ApplicationCore.Specifications.Catalog;

internal class SearchProductsListSpecification : SearchProductsListBaseSpecification
{
    public SearchProductsListSpecification(ProductsSearchData binocularsSearchData)
        : base(binocularsSearchData.FilterPredicate)
    {
        ApplyOrderBy(binocularsSearchData);

        ApplyPaging(binocularsSearchData.PageNumber, binocularsSearchData.PageSize);
    }

    protected override Dictionary<string, Expression<Func<Product, object>>> OrderByOptions =>
        new()
        {
            { "CreatedAt", t => t.CreatedAt },
            { "Price", t => t.Price },
        };
}