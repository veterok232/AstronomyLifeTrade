namespace ApplicationCore.Models.AccountProfile;

public class StatisticsQuery
{
    public Guid? ManagerAssignmentId { get; set; }
    
    public DateTime? CreatedOnFrom { get; set; }
    
    public DateTime? CreatedOnTo { get; set; }
}