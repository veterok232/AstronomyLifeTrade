using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

public record PostponeOrderCommand(Guid OrderId) : IRequest;