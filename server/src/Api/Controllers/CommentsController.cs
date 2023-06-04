using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.Comments.Get;
using ApplicationCore.Handlers.Comments.Publish;
using ApplicationCore.Handlers.Orders.Actions;
using ApplicationCore.Models.Comments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Routes.Comments.Get)]
    [Authorization(Roles.Consumer)]
    public Task<CommentsModel> Get(Guid productId)
    {
        return _mediator.Send(new GetProductCommentsQuery(productId));
    }
    
    [HttpPost(Routes.Comments.Publish)]
    [Authorization(Roles.Consumer)]
    public Task PublishComment([FromBody] PublishCommentModel model)
    {
        return _mediator.Send(new PublishCommentCommand(model));
    }
}