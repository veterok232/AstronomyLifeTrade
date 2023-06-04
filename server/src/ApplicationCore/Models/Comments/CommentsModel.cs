namespace ApplicationCore.Models.Comments;

public class CommentsModel
{
    public ICollection<CommentModel> Comments { get; set; }
    
    public int CommentsCount { get; set; }
    
    public double AverageRating { get; set; }
}