using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Common;

public class SearchModel : ISortable, IPageable
{
    public string SortBy { get; set; }
    
    public SortOrder Direction { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}