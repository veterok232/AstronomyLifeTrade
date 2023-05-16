using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class Promotion : Entity, IHasVersion, IHasCreatedAt, IHasModifiedAt
{
    public string Name { get; set; }

    public bool IsSpecial { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
    
    public decimal PromoRate { get; set; }
    
    public PromotionType PromotionType { get; set; }
    
    public string PromoCode { get; set; }
    
    public string Description { get; set; }

    public Guid Version { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
    
    public ICollection<PromotionProduct> Products { get; set; }
}