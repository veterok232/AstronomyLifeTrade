using MediatR;

namespace ApplicationCore.Handlers.Cart.Get;

public record GetProductsInCartQuery() : IRequest<ICollection<Guid>>;