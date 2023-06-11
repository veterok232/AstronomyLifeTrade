using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Management;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.DeleteProduct;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly ICreateProductService _createProductService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(
        ICreateProductService createProductService,
        IUnitOfWork unitOfWork)
    {
        _createProductService = createProductService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _createProductService.Delete(command.ProductId);
        
        await _unitOfWork.Commit();

        return result;
    }
}