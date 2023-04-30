namespace ApplicationCore.Entities.Interfaces;

public interface IHasDeletedAt
{
    public DateTime DeletedAt { get; set; } 
}