using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.GetPopularProducts;

public record GetPopularProductsRequest() : IRequest<ICollection<ProductListItem>>;