using ApplicationCore.Interfaces.Catalog;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.Get;

internal class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, ICollection<BrandModel>>
{
    private readonly ICatalogService _catalogService;

    public GetBrandsQueryHandler(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<ICollection<BrandModel>> Handle(
        GetBrandsQuery query,
        CancellationToken cancellationToken)
    {
        return await _catalogService.GetBrands();
    }
}