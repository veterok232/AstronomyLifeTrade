using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.EditProduct;

public record EditProductCommand(EditProductModel Model) : IRequest<Result<Guid>>;