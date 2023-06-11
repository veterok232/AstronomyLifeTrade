using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Models.Catalog;

public class CreateProductModel
{
    public string Name { get; set; }
    
    public string Code { get; set; }
    
    public string Description { get; set; }
    
    public Guid BrandId { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public decimal Price { get; set; }
    
    public string Manufacturer { get; set; }
    
    public int Quantity { get; set; }
    
    public string Equipment { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string? SpecialNote { get; set; }

    public ICollection<IFormFile>? ProductImages { get; set; }
    
    public ICollection<IFormFile>? ProductFiles { get; set; }
}