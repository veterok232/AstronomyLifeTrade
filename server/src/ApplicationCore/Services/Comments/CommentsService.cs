using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Comments;
using ApplicationCore.Models;
using ApplicationCore.Models.Comments;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Comments;
using AutoMapper;

namespace ApplicationCore.Services.Comments;

[ScopedDependency]
public class CommentsService : ICommentsService
{
    private readonly IRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;
    private readonly IAuthContextAccessor _authContextAccessor;

    public CommentsService(IRepository<Comment> commentsRepository, IMapper mapper, IAuthContextAccessor authContextAccessor)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
        _authContextAccessor = authContextAccessor;
    }

    public async Task<CommentsModel> GetComments(Guid productId)
    {
        var comments = await _commentsRepository.List(
            new CommentByProductIdSpecification(productId));

        return new CommentsModel
        {
            Comments = _mapper.Map<ICollection<CommentModel>>(comments),
            CommentsCount = comments.Count,
            AverageRating = comments.Select(c => c.Rating).Any()
                ? comments.Select(c => c.Rating).Average()
                : 0,
        };
    }

    public async Task PublishComment(PublishCommentModel model)
    {
        var comment = _mapper.Map<Comment>(model.Comment);
        comment.AssignmentId = _authContextAccessor.AssignmentId.Value;
        comment.ProductId = model.ProductId;

        await _commentsRepository.Add(comment);
    }
}