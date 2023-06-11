using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.CreateProduct;

public record CreateProductCharacteristicsCommand(ProductCharacteristics Model) : IRequest;