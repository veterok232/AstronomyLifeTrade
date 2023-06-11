using ApplicationCore.Interfaces.Catalog.Management;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.GetProductForEdit;

internal class GetProductForEditQueryHandler : IRequestHandler<GetProductForEditQuery, ProductForEditModel>
{
    private readonly ICreateProductService _createProductService;

    public GetProductForEditQueryHandler(ICreateProductService createProductService)
    {
        _createProductService = createProductService;
    }

    public async Task<ProductForEditModel> Handle(
        GetProductForEditQuery query,
        CancellationToken cancellationToken)
    {
        return await _createProductService.GetProductForEditModel(query.ProductId);
    }
}