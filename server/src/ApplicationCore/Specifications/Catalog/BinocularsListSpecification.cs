using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog.Search;

namespace ApplicationCore.Specifications.Catalog;

internal class BinocularsListSpecification : BinocularsListBaseSpecification
{
    public BinocularsListSpecification(BinocularsSearchData binocularsSearchData)
        : base(binocularsSearchData.FilterPredicate)
    {
        ApplyOrderBy(binocularsSearchData);

        ApplyPaging(binocularsSearchData.PageNumber, binocularsSearchData.PageSize);
    }

    protected override Dictionary<string, Expression<Func<Binocular, object>>> OrderByOptions =>
        new()
        {
            { "CreatedAt", t => t.Product.CreatedAt },
            { "Price", t => t.Product.Price },
        };
}