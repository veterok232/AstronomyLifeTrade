namespace ApplicationCore.Entities;

public class CommentFile : Entity
{
    public Guid FileId { get; set; }
    
    public File File { get; set; }
    
    public Guid CommentId { get; set; }
    
    public Comment Comment { get; set; }
}