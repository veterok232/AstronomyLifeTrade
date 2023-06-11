using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Management;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.EditProduct;

internal class EditProductCharacteristicsCommandHandler : AsyncRequestHandler<EditProductCharacteristicsCommand>
{
    private readonly ICreateProductService _createProductService;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCharacteristicsCommandHandler(
        ICreateProductService createProductService,
        IUnitOfWork unitOfWork)
    {
        _createProductService = createProductService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(
        EditProductCharacteristicsCommand command,
        CancellationToken cancellationToken)
    {
        await _createProductService.EditCharacteristics(command.Model);
        await _unitOfWork.Commit();
    }
}