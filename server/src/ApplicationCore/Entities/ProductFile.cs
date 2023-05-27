using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class ProductFile : Entity
{
    public Guid FileId { get; set; }
    
    public File File { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public ProductFileType ProductFileType { get; set; }
}