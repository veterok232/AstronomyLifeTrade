using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Catalog.Search;

public class BinocularSearchModel : ISortable, IPageable
{
    public IReadOnlyCollection<Guid>? BrandsIds { get; set; }
    
    public IReadOnlyCollection<FocusingMethod>? FocusingMethods { get; set; }
    
    public IReadOnlyCollection<OpticsMaterial>? OpticsMaterials { get; set; }
    
    public IReadOnlyCollection<BinocularPurpose>? BinocularPurposes { get; set; }
    
    public decimal? PriceMax { get; set; }
    
    public decimal? PriceMin { get; set; }

    public string SortBy { get; set; }
    
    public SortOrder Direction { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}