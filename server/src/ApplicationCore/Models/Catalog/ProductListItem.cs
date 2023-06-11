namespace ApplicationCore.Models.Catalog;

public class ProductListItem
{
    public Guid ProductId { get; set; }
    
    public BrandModel? Brand { get; set; }
    
    public CategoryModel? Category { get; set; }
    
    public decimal Price { get; set; }
    
    public int Quantity { get; set; }
    
    public string Name { get; set; }

    public string ShortDescription { get; set; }
    
    public string SpecialNote { get; set; }
    
    public string Code { get; set; }
    
    public ICollection<CharacteristicModel>? CharacteristicsModels { get; set; }
    
    public ProductRatingModel? ProductRating { get; set; }
    
    public ICollection<Guid>? ImageFilesIds { get; set; }
}