using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.EditProduct;

public record EditProductCharacteristicsCommand(ProductCharacteristics Model) : IRequest;