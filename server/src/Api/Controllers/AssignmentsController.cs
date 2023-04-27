using Api.Controllers.Attributes;
using ApplicationCore.Handlers.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[ApiRoute]
public class AssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<IEnumerable<GetUserAssignmentsResponse>> GetList() =>
        _mediator.Send(new Query<IEnumerable<GetUserAssignmentsResponse>>());
}