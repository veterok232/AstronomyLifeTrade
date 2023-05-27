using MediatR;

namespace ApplicationCore.Handlers.Cart.Clear;

public record ClearCartCommand() : IRequest;