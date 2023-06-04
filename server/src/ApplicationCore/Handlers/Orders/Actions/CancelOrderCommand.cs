using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

public record CancelOrderCommand(Guid OrderId) : IRequest;