using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Catalog.Search;

internal class TelescopesSearchData : ISortable, IPageable
{
    public string SortBy { get; init; }

    public SortOrder Direction { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public Expression<Func<Telescope, bool>> FilterPredicate { get; set; }
}