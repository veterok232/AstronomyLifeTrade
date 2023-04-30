using ApplicationCore.Enums;

namespace ApplicationCore.Models.Catalog.Search;

public class TelescopeSearchModel : ProductsSearchModel
{
    public ICollection<BrandModel> SelectedBrands { get; set; }
    
    public ICollection<TelescopeUserLevel> SelectedUserLevels { get; set; }
    
    public ICollection<TelescopeType> SelectedTelescopeTypes { get; set; }
    
    public ICollection<MountingType> SelectedMountingTypes { get; set; }
    
    public ICollection<ObservationLevel> SelectedObservableObjects { get; set; }
    
    public ICollection<TelescopeControlType> SelectedTelescopeControlTypes { get; set; }
}