namespace ApplicationCore.Models.Catalog;

public class ProductListItem
{
    public Guid BrandId { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public decimal Price { get; set; }
    
    public int Quantity { get; set; }
    
    public string Name { get; set; }

    public string ShortDescription { get; set; }
    
    public string SpecialNote { get; set; }
    
    public ICollection<CharacteristicModel> CharacteristicsModels { get; set; }
}