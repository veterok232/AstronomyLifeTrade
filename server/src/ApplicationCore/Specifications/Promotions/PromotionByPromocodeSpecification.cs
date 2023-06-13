using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Promotions;

public class PromotionByPromocodeSpecification : Specification<Promotion>
{
    public PromotionByPromocodeSpecification(string promocode)
        : base(p => p.PromoCode == promocode)
    {
    }
}