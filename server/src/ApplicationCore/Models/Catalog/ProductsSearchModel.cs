using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Catalog;

public class ProductsSearchModel : ISortable, IPageable
{
    public decimal PriceMin { get; set; }
    
    public decimal PriceMax { get; set; }
    
    public Guid CategoryId { get; set; }

    public string SortBy { get; }
    
    public SortOrder Direction { get; }
    
    public int PageNumber { get; }
    
    public int PageSize { get; }
}