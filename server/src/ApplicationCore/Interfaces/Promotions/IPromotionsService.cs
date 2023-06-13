using ApplicationCore.Models.Common;
using ApplicationCore.Models.Promotions;

namespace ApplicationCore.Interfaces.Promotions;

public interface IPromotionsService
{
    Task<Result<PromotionModel>> GetPromotion(string promocode);
}