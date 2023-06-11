using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchProducts;

internal class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, SearchResult<ProductListItem>>
{
    private readonly ICatalogSearchService _catalogSearchService;

    public SearchProductsRequestHandler(
        ICatalogSearchService catalogSearchService)
    {
        _catalogSearchService = catalogSearchService;
    }

    public async Task<SearchResult<ProductListItem>> Handle(
        SearchProductsRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _catalogSearchService.SearchProducts(request.Model);

        return products;
    }
}