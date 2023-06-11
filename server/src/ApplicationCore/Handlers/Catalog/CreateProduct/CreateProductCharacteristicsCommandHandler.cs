using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Management;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.CreateProduct;

internal class CreateProductCharacteristicsCommandHandler : AsyncRequestHandler<CreateProductCharacteristicsCommand>
{
    private readonly ICreateProductService _createProductService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCharacteristicsCommandHandler(
        ICreateProductService createProductService,
        IUnitOfWork unitOfWork)
    {
        _createProductService = createProductService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(
        CreateProductCharacteristicsCommand command,
        CancellationToken cancellationToken)
    {
        await _createProductService.CreateCharacteristics(command.Model);
        await _unitOfWork.Commit();
    }
}