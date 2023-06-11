using ApplicationCore.Interfaces.Cart;
using ApplicationCore.Interfaces.Catalog;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.AstronomicalCalculator.GetMostMatchingTelescopes;

internal class GetMostMatchingTelescopesQueryHandler
    : IRequestHandler<GetMostMatchingTelescopesQuery, ICollection<ProductListItem>>
{
    private readonly ICatalogSearchService _catalogSearchService;

    public GetMostMatchingTelescopesQueryHandler(ICatalogSearchService catalogSearchService)
    {
        _catalogSearchService = catalogSearchService;
    }

    public async Task<ICollection<ProductListItem>> Handle(
        GetMostMatchingTelescopesQuery query,
        CancellationToken cancellationToken)
    {
        return await _catalogSearchService.GetTelescopesForCalculator(query.Model);
    }
}