using MediatR;

namespace ApplicationCore.Handlers.Cart.Add;

public record AddProductToCartCommand(Guid ProductId) : IRequest;