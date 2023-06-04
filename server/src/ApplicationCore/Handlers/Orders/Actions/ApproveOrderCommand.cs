using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

public record ApproveOrderCommand(Guid OrderId) : IRequest;