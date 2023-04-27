using MediatR;

namespace ApplicationCore.Handlers.Common;

public record Query<T> : IRequest<T>
{
}