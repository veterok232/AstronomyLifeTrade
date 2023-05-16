using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Brand : Entity
{
    public string Name { get; set; }
    
    public CategoryType CategoryType { get; set; }
    
    public string Code { get; set; }
}