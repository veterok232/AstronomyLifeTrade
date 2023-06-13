using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Handlers.Orders.Search;
using ApplicationCore.Handlers.Promotions.GetPromotion;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Orders;
using ApplicationCore.Models.Promotions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class PromotionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PromotionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Routes.Promotions.GetPromotion)]
    [Authorization(Roles.Consumer)]
    public Task<Result<PromotionModel>> GetPromotion(string promocode)
    {
        return _mediator.Send(new GetPromotionQuery(promocode));
    }
}