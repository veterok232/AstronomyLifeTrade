using MediatR;

namespace ApplicationCore.Handlers.Common;

public interface IQueryHandler<T> : IRequestHandler<Query<T>, T>
{
}