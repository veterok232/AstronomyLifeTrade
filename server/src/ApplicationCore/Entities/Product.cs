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
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
    
    public DateTime DeletedAt { get; set; }
}