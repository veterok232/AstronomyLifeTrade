using ApplicationCore.Models.Common;
using ApplicationCore.Models.Promotions;
using MediatR;

namespace ApplicationCore.Handlers.Promotions.GetPromotion;

public record GetPromotionQuery(string Promocode) : IRequest<Result<PromotionModel>>;