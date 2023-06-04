using ApplicationCore.Models.Comments;
using MediatR;

namespace ApplicationCore.Handlers.Comments.Get;

public record GetProductCommentsQuery(Guid ProductId) : IRequest<CommentsModel>;