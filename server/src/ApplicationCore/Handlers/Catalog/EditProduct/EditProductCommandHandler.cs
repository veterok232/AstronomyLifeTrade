using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Management;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.EditProduct;

internal class EditProductCommandHandler : IRequestHandler<EditProductCommand, Result<Guid>>
{
    private readonly ICreateProductService _createProductService;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCommandHandler(
        ICreateProductService createProductService,
        IUnitOfWork unitOfWork)
    {
        _createProductService = createProductService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        EditProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _createProductService.Edit(command.Model);
        
        await _unitOfWork.Commit();

        return result;
    }
}