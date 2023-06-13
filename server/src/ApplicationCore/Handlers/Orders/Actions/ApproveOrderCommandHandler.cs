using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Actions;

internal class ApproveOrderCommandHandler : IRequestHandler<ApproveOrderCommand, Result>
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

    public async Task<Result> Handle(
        ApproveOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _ordersService.ApproveOrder(command.OrderId);

        await _unitOfWork.Commit();

        return result;
    }
}