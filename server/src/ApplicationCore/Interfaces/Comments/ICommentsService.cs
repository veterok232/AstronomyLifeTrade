using ApplicationCore.Models.Comments;

namespace ApplicationCore.Interfaces.Comments;

public interface ICommentsService
{
    Task<CommentsModel> GetComments(Guid productId);
    
    Task PublishComment(PublishCommentModel model);
}