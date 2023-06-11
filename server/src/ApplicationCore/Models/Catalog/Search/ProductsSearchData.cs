using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Catalog.Search;

public class ProductsSearchData : ISortable, IPageable
{
    public string SortBy { get; init; }

    public SortOrder Direction { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public Expression<Func<Product, bool>> FilterPredicate { get; set; }
}