using ApplicationCore.Entities;
using ApplicationCore.Enums;

namespace ApplicationCore.Models.Promotions;

public class PromotionModel
{
    public string Name { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
    
    public decimal PromoRate { get; set; }
    
    public PromotionType PromotionType { get; set; }
    
    public string? PromoCode { get; set; }
}