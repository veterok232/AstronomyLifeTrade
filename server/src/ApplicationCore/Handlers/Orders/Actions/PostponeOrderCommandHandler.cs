using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

internal class PostponeOrderCommandHandler : AsyncRequestHandler<PostponeOrderCommand>
{
    private readonly IOrdersService _ordersService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public PostponeOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IMediator mediator,
        IOrdersService ordersService)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _ordersService = ordersService;
    }

    protected override async Task Handle(
        PostponeOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _ordersService.PostponeOrder(command.OrderId);

        await _unitOfWork.Commit();
    }
}