using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Product : Entity, IHasCreatedAt, IHasModifiedAt, IHasDeletedAt
{
    public Guid BrandId { get; set; }
    
    public Brand Brand { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public Category Category { get; set; }
    
    public string Code { get; set; }
    
    public decimal Price { get; set; }
    
    public string Manufacturer { get; set; }
    
    public int Quantity { get; set; }
    
    public string Name { get; set; }

    public string ShortDescription { get; set; }
    
    public string SpecialNote { get; set; }
    
    public string Description { get; set; }
    
    public string Equipment { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    
    public ICollection<ProductItem> ProductItems { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
    
    public ICollection<ProductFile> Files { get; set; }
}