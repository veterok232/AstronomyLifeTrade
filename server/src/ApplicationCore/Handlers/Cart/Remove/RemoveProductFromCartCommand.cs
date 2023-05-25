using MediatR;

namespace ApplicationCore.Handlers.Cart.Remove;

public record RemoveProductFromCartCommand(Guid ProductId) : IRequest;