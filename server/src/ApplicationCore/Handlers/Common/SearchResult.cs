namespace ApplicationCore.Handlers.Common;

public class SearchResult<TResult>
{
    public IReadOnlyList<TResult> Items { get; set; }

    public int TotalCount { get; set; }
}