using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.AstronomicalCalculator.GetMostMatchingTelescopes;
using ApplicationCore.Models.AstronomicalCalculator;
using ApplicationCore.Models.Catalog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class AstronomicalCalculatorController : ControllerBase
{
    private readonly IMediator _mediator;

    public AstronomicalCalculatorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Routes.AstronomicalCalculator.GetMostMatchingTelescopes)]
    [Authorization(Roles.Consumer)]
    public Task<ICollection<ProductListItem>> GetMostMatchingTelescopes(
        [FromQuery] AstronomicalCalculatorMostMatchingModel model)
    {
        return _mediator.Send(new GetMostMatchingTelescopesQuery(model));
    }
}