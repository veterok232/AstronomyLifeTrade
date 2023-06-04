using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;

namespace ApplicationCore.Specifications.Catalog;

internal class TelescopesListSpecification : TelescopesListBaseSpecification
{
    public TelescopesListSpecification(TelescopesSearchData telescopesSearchData)
        : base(telescopesSearchData.FilterPredicate)
    {
        ApplyOrderBy(telescopesSearchData);

        ApplyPaging(telescopesSearchData.PageNumber, telescopesSearchData.PageSize);
    }

    protected override Dictionary<string, Expression<Func<Telescope, object>>> OrderByOptions =>
        new()
        {
            { "CreatedAt", t => t.Product.CreatedAt },
            { "Price", t => t.Product.Price },
        };
}