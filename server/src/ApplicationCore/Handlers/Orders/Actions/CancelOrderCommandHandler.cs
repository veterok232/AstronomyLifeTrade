using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

internal class CancelOrderCommandHandler : AsyncRequestHandler<CancelOrderCommand>
{
    private readonly IOrdersService _ordersService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CancelOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IMediator mediator,
        IOrdersService ordersService)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _ordersService = ordersService;
    }

    protected override async Task Handle(
        CancelOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _ordersService.CancelOrder(command.OrderId);

        await _unitOfWork.Commit();
    }
}