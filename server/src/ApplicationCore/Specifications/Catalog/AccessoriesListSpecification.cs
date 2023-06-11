using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog.Search;

namespace ApplicationCore.Specifications.Catalog;

internal class AccessoriesListSpecification : AccessoriesListBaseSpecification
{
    public AccessoriesListSpecification(AccessoriesSearchData accessoriesSearchData)
        : base(accessoriesSearchData.FilterPredicate)
    {
        ApplyOrderBy(accessoriesSearchData);

        ApplyPaging(accessoriesSearchData.PageNumber, accessoriesSearchData.PageSize);
    }

    protected override Dictionary<string, Expression<Func<Accessory, object>>> OrderByOptions =>
        new()
        {
            { "CreatedAt", t => t.Product.CreatedAt },
            { "Price", t => t.Product.Price },
        };
}