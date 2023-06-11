using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.GetPopularProducts;

internal class GetPopularProductsRequestHandler : IRequestHandler<GetPopularProductsRequest, ICollection<ProductListItem>>
{
    private readonly IProductsSearchService _productsSearchService;

    public GetPopularProductsRequestHandler(IProductsSearchService productsSearchService)
    {
        _productsSearchService = productsSearchService;
    }

    public async Task<ICollection<ProductListItem>> Handle(
        GetPopularProductsRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _productsSearchService.GetPopularProducts();

        return products;
    }
}