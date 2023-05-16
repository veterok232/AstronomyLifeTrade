namespace ApplicationCore.Entities;

public class PromotionProduct : Entity
{
    public Guid PromotionId { get; set; }
    
    public Promotion Promotion { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
}