using MediatR;

namespace ApplicationCore.Handlers.Common;

internal interface IEntityQueryHandler<T> : IRequestHandler<EntityQuery<T>, T>
{
}