using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchAccessories;

internal class SearchAccessoriesRequestHandler : IRequestHandler<SearchAccessoriesRequest, SearchResult<ProductListItem>>
{
    private readonly ICatalogSearchService _catalogSearchService;

    public SearchAccessoriesRequestHandler(
        ICatalogSearchService catalogSearchService)
    {
        _catalogSearchService = catalogSearchService;
    }

    public async Task<SearchResult<ProductListItem>> Handle(
        SearchAccessoriesRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _catalogSearchService.GetAccessories(request.Model);

        return products;
    }
}