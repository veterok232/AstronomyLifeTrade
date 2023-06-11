using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Management;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.CreateProduct;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly ICreateProductService _createProductService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        ICreateProductService createProductService,
        IUnitOfWork unitOfWork)
    {
        _createProductService = createProductService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _createProductService.Create(command.Model);
        
        await _unitOfWork.Commit();

        return result;
    }
}