using ApplicationCore.Interfaces.Promotions;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Promotions;
using MediatR;

namespace ApplicationCore.Handlers.Promotions.GetPromotion;

internal class GetPromotionQueryHandler : IRequestHandler<GetPromotionQuery, Result<PromotionModel>>
{
    private readonly IPromotionsService _promotionsService;

    public GetPromotionQueryHandler(IPromotionsService promotionsService)
    {
        _promotionsService = promotionsService;
    }

    public async Task<Result<PromotionModel>> Handle(
        GetPromotionQuery query,
        CancellationToken cancellationToken)
    {
        return await _promotionsService.GetPromotion(query.Promocode);
    }
}