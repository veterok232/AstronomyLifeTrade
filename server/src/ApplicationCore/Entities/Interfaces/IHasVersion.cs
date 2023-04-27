namespace ApplicationCore.Entities.Interfaces;

public interface IHasVersion
{
    Guid Version { get; set; }
}