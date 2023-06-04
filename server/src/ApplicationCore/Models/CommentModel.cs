using ApplicationCore.Models.Catalog;

namespace ApplicationCore.Models;

public class CommentModel
{
    public string Text { get; set; }

    public double Rating { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string? UserName { get; set; }
    
    public string? UserLastName { get; set; }
}