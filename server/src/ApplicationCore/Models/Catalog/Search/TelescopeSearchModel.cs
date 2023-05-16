using ApplicationCore.Enums;

namespace ApplicationCore.Models.Catalog.Search;

public class TelescopeSearchModel : ProductsSearchModel
{
    public ICollection<Guid> BrandsIds { get; set; }
    
    public ICollection<TelescopeUserLevel> UserLevels { get; set; }
    
    public ICollection<TelescopeType> TelescopeTypes { get; set; }
    
    public ICollection<MountingType> MountingTypes { get; set; }

    public ICollection<TelescopeControlType> TelescopeControlTypes { get; set; }
}