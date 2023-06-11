using ApplicationCore.Entities;
using ApplicationCore.Enums;

namespace ApplicationCore.Models.Catalog;

public class TelescopeCharacteristics
{
    public decimal? Aperture { get; set; }
    
    public decimal? ApertureRatio { get; set; }
    
    public decimal? EyepieceFittingDiameter { get; set; }
    
    public decimal? FocusDistance { get; set; }
    
    public decimal? MaxUsefulScale { get; set; }

    public decimal? MinUsefulScale { get; set; }

    public MountingType? MountingType { get; set; }
    
    public TelescopeControlType? TelescopeControlType { get; set; }
    
    public decimal? ScaleMax { get; set; }
    
    public decimal? ScaleMin { get; set; }
    
    public string? Seeker { get; set; }
    
    public string? TripodHeight { get; set; }
    
    public string? TripodMaterial { get; set; }
    
    public TelescopeType? Type { get; set; }
    
    public TelescopeUserLevel? TelescopeUserLevel { get; set; }
    
    public decimal? Weight { get; set; }

    public ICollection<TelescopeEyepiece>? TelescopeEyepieces { get; set; }
}