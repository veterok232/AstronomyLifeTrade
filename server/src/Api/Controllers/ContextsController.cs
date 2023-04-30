using Api.Controllers.Attributes;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Handlers.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[ApiRoute]
[Authorize]
public class ContextsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContextsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public Task<ContextResponse> Get()
    {
        return _mediator.Send(new Query<ContextResponse>());
    }
}