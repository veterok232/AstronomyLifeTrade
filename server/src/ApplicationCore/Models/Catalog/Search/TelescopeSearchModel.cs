using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Catalog.Search;

public class TelescopeSearchModel : ISortable, IPageable
{
    public IReadOnlyCollection<Guid>? BrandsIds { get; set; }
    
    public IReadOnlyCollection<TelescopeType>? TelescopeTypes { get; set; }
    
    public IReadOnlyCollection<TelescopeUserLevel>? UserLevels { get; set; }
    
    public IReadOnlyCollection<MountingType>? MountingTypes { get; set; }
    
    public IReadOnlyCollection<TelescopeControlType>? TelescopeControlTypes { get; set; }
    
    public decimal? PriceMax { get; set; }
    
    public decimal? PriceMin { get; set; }

    public string SortBy { get; set; }
    
    public SortOrder Direction { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}