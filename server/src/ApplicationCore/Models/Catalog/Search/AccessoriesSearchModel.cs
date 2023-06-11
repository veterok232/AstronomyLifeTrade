using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Catalog.Search;

public class AccessoriesSearchModel : ISortable, IPageable
{
    public IReadOnlyCollection<Guid>? BrandsIds { get; set; }
    
    public IReadOnlyCollection<AccessoryType>? AccesoryTypes { get; set; }
    
    public decimal? PriceMax { get; set; }
    
    public decimal? PriceMin { get; set; }
    
    public string SortBy { get; set; }
    
    public SortOrder Direction { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}