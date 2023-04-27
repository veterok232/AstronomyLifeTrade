namespace ApplicationCore.Entities.Interfaces;

public interface IHasCreatedByUser
{
    Guid CreatedByUserId { get; set; }
}