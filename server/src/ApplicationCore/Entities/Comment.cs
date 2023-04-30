using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Comment : Entity, IHasCreatedAt
{
    public Guid AssignmentId { get; set; }
    
    public Assignment Assignment { get; set; }
    
    public string Text { get; set; }
    
    public int Rating { get; set; }
    
    
    
    public DateTime CreatedAt { get; set; }
}