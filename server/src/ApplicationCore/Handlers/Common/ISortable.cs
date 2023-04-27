using ApplicationCore.Enums;

namespace ApplicationCore.Handlers.Common;

public interface ISortable
{
    string SortBy { get; }

    SortOrder Direction { get; }
}