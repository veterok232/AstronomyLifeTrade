using ApplicationCore.Enums;

namespace ApplicationCore.Handlers.Common;

public record ListQuery<T> : Query<T>, ISortable, IPageable
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public string SortBy { get; init; }

    public SortOrder Direction { get; init; }
}