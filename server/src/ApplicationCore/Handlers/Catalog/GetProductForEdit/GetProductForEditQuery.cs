using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.GetProductForEdit;

public record GetProductForEditQuery(Guid ProductId) : IRequest<ProductForEditModel>;