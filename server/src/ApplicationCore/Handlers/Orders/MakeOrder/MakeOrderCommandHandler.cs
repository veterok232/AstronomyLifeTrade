using ApplicationCore.Entities;
using ApplicationCore.Handlers.Cart.Clear;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Common;
using ApplicationCore.Utils;
using MediatR;

namespace ApplicationCore.Handlers.Orders.MakeOrder;

internal class MakeOrderCommandHandler : IRequestHandler<MakeOrderCommand, Result<int>>
{
    private readonly IMakeOrderService _makeOrderService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Order> _orderRepository;
    private readonly IMediator _mediator;

    public MakeOrderCommandHandler(
        IMakeOrderService makeOrderService,
        IUnitOfWork unitOfWork,
        IRepository<Order> orderRepository,
        IMediator mediator)
    {
        _makeOrderService = makeOrderService;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _mediator = mediator;
    }

    public async Task<Result<int>> Handle(
        MakeOrderCommand query,
        CancellationToken cancellationToken)
    {
        var result = await _makeOrderService.MakeOrder(query.Model);

        await _unitOfWork.Commit();

        if (result.IsSucceeded)
        {
            await _mediator.Send(new ClearCartCommand());

            return ResultBuilder.BuildSucceeded((await _orderRepository.GetById(result.Data.Id)).OrderNumber);
        }

        return ResultBuilder.RebuildData(result, 0);
    }
}