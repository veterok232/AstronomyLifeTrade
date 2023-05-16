namespace ApplicationCore.Models.Catalog;

public class CategoryModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Code { get; set; }
    
    public string Description { get; set; }
    
    public int ProductsCount { get; set; }
}