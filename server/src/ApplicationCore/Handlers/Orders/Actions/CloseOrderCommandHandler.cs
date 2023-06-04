using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

internal class CloseOrderCommandHandler : AsyncRequestHandler<CloseOrderCommand>
{
    private readonly IOrdersService _ordersService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CloseOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IMediator mediator,
        IOrdersService ordersService)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _ordersService = ordersService;
    }

    protected override async Task Handle(
        CloseOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _ordersService.CloseOrder(command.OrderId);

        await _unitOfWork.Commit();
    }
}