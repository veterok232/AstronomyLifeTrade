using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest<Result>;