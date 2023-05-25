using MediatR;

namespace ApplicationCore.Handlers.Catalog.ProductDetails;

public record GetProductDetailsQuery(Guid ProductId) : IRequest<Models.Catalog.ProductDetails>;