namespace ApplicationCore.Handlers.Common;

public record EntityQuery<T> : Query<T>
{
    public EntityQuery(Guid id) => Id = id;

    public Guid Id { get; }
}