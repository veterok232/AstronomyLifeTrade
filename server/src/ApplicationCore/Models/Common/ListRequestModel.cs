using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Common;

public class ListRequestModel : ISortable, IPageable
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string SortBy { get; set; }

    public SortOrder Direction { get; set; }
}