namespace ApplicationCore.Models.Catalog;

public class ProductRatingModel
{
    public Guid ProductId { get; set; }
    
    public decimal Rating { get; set; }
    
    public int CommentsCount { get; set; }
}