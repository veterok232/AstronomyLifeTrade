using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Binocular : Entity
{
    public decimal Aperture { get; set; }
    
    public decimal ExitPupilDiameterMax { get; set; }
    
    public decimal ExitPupilDiameterMin { get; set; }
    
    public FocusingMethod FocusingMethod { get; set; }
    
    public decimal FovMin { get; set; }
    
    public decimal FovMax { get; set; }
    
    public decimal HasAdapter { get; set; }
    
    public decimal HasCase { get; set; }
    
    public decimal HasMoistureProtection { get; set; }
    
    public decimal InterpupillaryDistanseMin { get; set; }
    
    public decimal InterpupillaryDistanseMax { get; set; }
    
    public decimal FocusDistanceMin { get; set; }
    
    public OpticsMaterial OpticsMaterial { get; set; }
    
    public PrismType PrismType { get; set; }
    
    public BinocularPurpose Purpose { get; set; }
    
    public decimal RelativeBrightnessMin { get; set; }
    
    public decimal RelativeBrightnessMax { get; set; }
    
    public decimal RemovalExitPupilMin { get; set; }
    
    public decimal RemovalExitPupilMax { get; set; }
    
    public decimal ScaleMin { get; set; }
    
    public decimal ScaleMax { get; set; }
    
    public BinocularSize Size { get; set; }
    
    public decimal Weight { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
}