using ApplicationCore.Interfaces.Catalog;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.Get;

internal class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ICollection<CategoryModel>>
{
    private readonly ICatalogService _catalogService;

    public GetCategoriesQueryHandler(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<ICollection<CategoryModel>> Handle(
        GetCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        return await _catalogService.GetCategories();;
    }
}