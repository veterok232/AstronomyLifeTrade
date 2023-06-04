using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

internal class ApproveOrderCommandHandler : AsyncRequestHandler<ApproveOrderCommand>
{
    private readonly IOrdersService _ordersService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public ApproveOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IMediator mediator,
        IOrdersService ordersService)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _ordersService = ordersService;
    }

    protected override async Task Handle(
        ApproveOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _ordersService.ApproveOrder(command.OrderId);

        await _unitOfWork.Commit();
    }
}