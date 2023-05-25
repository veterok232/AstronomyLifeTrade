using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Catalog;

public class ProductsSearchModel : ISortable, IPageable
{
    public decimal? PriceMin { get; set; }
    
    public decimal? PriceMax { get; set; }

    public string SortBy { get; set; }
    
    public SortOrder Direction { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}