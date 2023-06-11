using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.CreateProduct;

public record CreateProductCommand(CreateProductModel Model) : IRequest<Result<Guid>>;