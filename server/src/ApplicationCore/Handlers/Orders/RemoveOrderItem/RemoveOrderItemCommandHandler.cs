using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.RemoveOrderItem;

internal class RemoveOrderItemCommandHandler : AsyncRequestHandler<RemoveOrderItemCommand>
{
    private readonly IOrdersService _ordersService;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderItemCommandHandler(IOrdersService ordersService, IUnitOfWork unitOfWork)
    {
        _ordersService = ordersService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(RemoveOrderItemCommand command, CancellationToken cancellationToken)
    {
        await _ordersService.RemoveOrderItem(command.Model);
        await _unitOfWork.Commit();
    }
}