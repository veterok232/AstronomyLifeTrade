using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchProducts;

public record SearchProductsRequest(ProductsSearchModel Model) : IRequest<SearchResult<ProductListItem>>;