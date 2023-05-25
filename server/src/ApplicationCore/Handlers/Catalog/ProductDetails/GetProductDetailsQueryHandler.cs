using ApplicationCore.Interfaces.Catalog.ProductDetails;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.ProductDetails;

internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, Models.Catalog.ProductDetails>
{
    private readonly IProductDetailsService _productDetailsService;

    public GetProductDetailsQueryHandler(IProductDetailsService productDetailsService)
    {
        _productDetailsService = productDetailsService;
    }

    public async Task<Models.Catalog.ProductDetails> Handle(
        GetProductDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _productDetailsService.Get(query.ProductId);

        return products;
    }
}