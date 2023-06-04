using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

public record CloseOrderCommand(Guid OrderId) : IRequest;