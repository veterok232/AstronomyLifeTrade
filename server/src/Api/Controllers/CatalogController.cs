using Api.Constants;
using Api.Controllers.Attributes;
using Api.Interfaces;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator, IRefreshTokenCookieService refreshTokenCookieService)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Routes.Catalog.Get)]
    [AllowAnonymous]
    public Task<SearchResult<ProductListItem>> Get([FromQuery] ProductsSearchModel model)
    {
        return _mediator.Send(new GetProductsRequest(model));
    }
}