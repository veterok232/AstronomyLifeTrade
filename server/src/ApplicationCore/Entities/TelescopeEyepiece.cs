namespace ApplicationCore.Entities;

public class TelescopeEyepiece : Entity
{
    public string Name { get; set; }
    
    public Guid TelescopeId { get; set; }
    
    public Telescope Telescope { get; set; }
    
    public decimal EffectiveScale { get; set; }
}