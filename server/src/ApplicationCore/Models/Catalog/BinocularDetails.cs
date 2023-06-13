using ApplicationCore.Enums;

namespace ApplicationCore.Models.Catalog;

public class BinocularDetails : ProductDetails
{
    public decimal? Aperture { get; set; }
    
    public decimal? ExitPupilDiameterMax { get; set; }
    
    public decimal? ExitPupilDiameterMin { get; set; }
    
    public FocusingMethod? FocusingMethod { get; set; }
    
    public decimal? FovMin { get; set; }
    
    public decimal? FovMax { get; set; }
    
    public string? HasAdapter { get; set; }
    
    public string? HasCase { get; set; }
    
    public string? HasMoistureProtection { get; set; }
    
    public decimal? InterpupillaryDistanseMin { get; set; }
    
    public decimal? InterpupillaryDistanseMax { get; set; }
    
    public decimal? FocusDistanceMin { get; set; }
    
    public OpticsMaterial? OpticsMaterial { get; set; }
    
    public PrismType? PrismType { get; set; }
    
    public BinocularPurpose? BinocularPurpose { get; set; }
    
    public decimal? RelativeBrightnessMin { get; set; }
    
    public decimal? RelativeBrightnessMax { get; set; }
    
    public decimal? RemovalExitPupilMin { get; set; }
    
    public decimal? RemovalExitPupilMax { get; set; }
    
    public decimal? ScaleMin { get; set; }
    
    public decimal? ScaleMax { get; set; }

    public decimal? Weight { get; set; }
}