namespace ApplicationCore.Models.Catalog;

public class ProductCharacteristics
{
    public Guid ProductId { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public TelescopeCharacteristics? TelescopeCharacteristics { get; set; }
    
    public BinocularCharacteristics? BinocularCharacteristics { get; set; }
    
    public AccessoryCharacteristics? AccessoryCharacteristics { get; set; }
}