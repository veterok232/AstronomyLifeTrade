using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Promotions;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Promotions;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Promotions;
using ApplicationCore.Utils;
using AutoMapper;

namespace ApplicationCore.Services.Promotions;

[ScopedDependency]
public class PromotionsService : IPromotionsService
{
    private readonly IRepository<Promotion> _promotionsRepository;
    private readonly IMapper _mapper;

    public PromotionsService(IRepository<Promotion> promotionsRepository, IMapper mapper)
    {
        _promotionsRepository = promotionsRepository;
        _mapper = mapper;
    }

    public async Task<Result<PromotionModel>> GetPromotion(string promocode)
    {
        var promotion = await _promotionsRepository.GetSingleOrDefault(
            new PromotionByPromocodeSpecification(promocode));

        if (promotion == null)
        {
            return ResultBuilder.BuildFailedWithData(new PromotionModel(), "Данный промокод недоступен или не существует");
        }

        return ResultBuilder.BuildSucceeded(_mapper.Map<PromotionModel>(promotion));
    }
}