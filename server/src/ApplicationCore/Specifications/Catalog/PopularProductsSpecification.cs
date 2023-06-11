using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.Common;

namespace ApplicationCore.Specifications.Catalog;

internal class PopularProductsSpecification : PopularProductsBaseSpecification
{
    public PopularProductsSpecification()
        : base(_ => true)
    {
        var searchModel = new SearchModel
        {
            SortBy = "SoldCount",
            Direction = SortOrder.Descending,
            PageNumber = 1,
            PageSize = 10,
        };
        
        ApplyOrderBy(searchModel);

        ApplyPaging(searchModel.PageNumber, searchModel.PageSize);
    }

    protected override Dictionary<string, Expression<Func<Product, object>>> OrderByOptions =>
        new()
        {
            { "SoldCount", p => p.SoldCount },
        };
}