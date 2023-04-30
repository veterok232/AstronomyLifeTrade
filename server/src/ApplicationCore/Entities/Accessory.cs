using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Accessory : Entity
{
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public AccessoryType AccessoryType { get; set; }
}