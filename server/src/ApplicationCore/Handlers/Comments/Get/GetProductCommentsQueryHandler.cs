using ApplicationCore.Interfaces.Comments;
using ApplicationCore.Models.Comments;
using MediatR;

namespace ApplicationCore.Handlers.Comments.Get;

internal class GetProductCommentsQueryHandler : IRequestHandler<GetProductCommentsQuery, CommentsModel>
{
    private readonly ICommentsService _commentsService;

    public GetProductCommentsQueryHandler(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    public async Task<CommentsModel> Handle(
        GetProductCommentsQuery query,
        CancellationToken cancellationToken)
    {
        var commentsModel = await _commentsService.GetComments(query.ProductId);

        return commentsModel;
    }
}