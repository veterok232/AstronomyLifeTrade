using ApplicationCore.Handlers.Catalog.GetPopularProducts;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchTelescopes;

internal class SearchTelescopesRequestHandler : IRequestHandler<SearchTelescopesRequest, SearchResult<ProductListItem>>
{
    private readonly ICatalogSearchService _catalogSearchService;

    public SearchTelescopesRequestHandler(
        ICatalogSearchService catalogSearchService)
    {
        _catalogSearchService = catalogSearchService;
    }

    public async Task<SearchResult<ProductListItem>> Handle(
        SearchTelescopesRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _catalogSearchService.GetPopularProducts();

        return products;
    }
}