using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchTelescopes;

public record SearchTelescopesRequest(TelescopeSearchModel Model) : IRequest<SearchResult<ProductListItem>>;