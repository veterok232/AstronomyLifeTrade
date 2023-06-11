using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchBinoculars;

internal class SearchBinocularsRequestHandler : IRequestHandler<SearchBinocularsRequest, SearchResult<ProductListItem>>
{
    private readonly ICatalogSearchService _catalogSearchService;

    public SearchBinocularsRequestHandler(
        ICatalogSearchService catalogSearchService)
    {
        _catalogSearchService = catalogSearchService;
    }

    public async Task<SearchResult<ProductListItem>> Handle(
        SearchBinocularsRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _catalogSearchService.GetBinoculars(request.Model);

        return products;
    }
}