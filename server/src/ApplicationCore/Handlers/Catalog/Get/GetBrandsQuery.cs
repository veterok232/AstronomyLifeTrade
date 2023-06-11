using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.Get;

public record GetBrandsQuery : IRequest<ICollection<BrandModel>>;