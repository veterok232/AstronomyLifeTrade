using ApplicationCore.Enums;

namespace ApplicationCore.Models.Catalog;

public class BrandModel
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public CategoryType? CategoryType { get; set; }
}