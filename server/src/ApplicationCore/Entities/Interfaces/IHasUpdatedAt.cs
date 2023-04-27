namespace ApplicationCore.Entities.Interfaces;

public interface IHasUpdatedAt
{
    DateTime? UpdatedAt { get; set; }
}