namespace ApplicationCore.Models.Comments;

public class PublishCommentModel
{
    public Guid ProductId { get; set; }
    
    public CommentModel Comment { get; set; }
}