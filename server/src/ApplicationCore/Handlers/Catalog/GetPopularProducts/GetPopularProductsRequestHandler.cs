using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.GetPopularProducts;

internal class GetPopularProductsRequestHandler : IRequestHandler<GetPopularProductsRequest, ICollection<ProductListItem>>
{
    private readonly ICatalogSearchService _catalogSearchService;

    public GetPopularProductsRequestHandler(
        ICatalogSearchService catalogSearchService)
    {
        _catalogSearchService = catalogSearchService;
    }

    public async Task<ICollection<ProductListItem>> Handle(
        GetPopularProductsRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _catalogSearchService.GetPopularProducts();

        return products;
    }
}