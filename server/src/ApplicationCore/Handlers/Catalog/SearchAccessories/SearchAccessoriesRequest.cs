using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchAccessories;

public record SearchAccessoriesRequest(AccessoriesSearchModel Model) : IRequest<SearchResult<ProductListItem>>;