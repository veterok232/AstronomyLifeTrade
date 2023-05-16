using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class ProductItem : Entity, IHasCreatedAt, IHasModifiedAt, IHasDeletedAt
{
    public bool IsSold { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public string SerialNumber { get; set; }
    
    public string Sku { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
}