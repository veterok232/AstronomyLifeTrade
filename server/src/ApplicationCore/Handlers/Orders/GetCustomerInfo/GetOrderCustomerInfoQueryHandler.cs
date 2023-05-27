using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.GetCustomerInfo;

internal class GetOrderCustomerInfoQueryHandler : IRequestHandler<GetOrderCustomerInfoQuery, OrderCustomerInfo>
{
    private readonly IOrderCustomerInfoService _orderCustomerInfoService;

    public GetOrderCustomerInfoQueryHandler(IOrderCustomerInfoService orderCustomerInfoService)
    {
        _orderCustomerInfoService = orderCustomerInfoService;
    }

    public async Task<OrderCustomerInfo> Handle(
        GetOrderCustomerInfoQuery query,
        CancellationToken cancellationToken)
    {
        var orderCustomerInfo = await _orderCustomerInfoService.Get();

        return orderCustomerInfo;
    }
}