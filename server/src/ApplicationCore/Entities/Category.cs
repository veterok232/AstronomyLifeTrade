namespace ApplicationCore.Entities;

public class Category : Entity
{
    public string Name { get; set; }
    
    public string Code { get; set; }
    
    public string Description { get; set; }
    
    public int ProductsCount { get; set; }
}