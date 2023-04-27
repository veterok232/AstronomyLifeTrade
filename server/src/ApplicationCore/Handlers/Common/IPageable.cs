namespace ApplicationCore.Handlers.Common;

public interface IPageable
{
    int PageNumber { get; }

    int PageSize { get; }
}