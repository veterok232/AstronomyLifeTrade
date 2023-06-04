using ApplicationCore.Models.Files;

namespace ApplicationCore.Models.Catalog;

public class ProductDetails
{
    public BrandModel Brand { get; set; }

    public CategoryModel Category { get; set; }

    public string Code { get; set; }

    public decimal Price { get; set; }

    public string Manufacturer { get; set; }

    public int Quantity { get; set; }

    public string Name { get; set; }

    public string ShortDescription { get; set; }

    public string Description { get; set; }

    public string Equipment { get; set; }
    
    public ICollection<CommentModel>? Comments { get; set; }
    
    public ProductRatingModel Rating { get; set; }
    
    public ICollection<CharacteristicModel> Characteristics { get; set; }
    
    public ICollection<Guid>? ProductImagesIds { get; set; }
    
    public ICollection<FileModel>? ProductFiles { get; set; }
}