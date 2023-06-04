using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Details;

internal class GetOrderDetailsModelQueryHandler : IRequestHandler<GetOrderDetailsModelQuery, OrderDetailsModel>
{
    private readonly IOrdersSearchService _ordersSearchService;

    public GetOrderDetailsModelQueryHandler(IOrdersSearchService ordersSearchService)
    {
        _ordersSearchService = ordersSearchService;
    }

    public async Task<OrderDetailsModel> Handle(
        GetOrderDetailsModelQuery query,
        CancellationToken cancellationToken)
    {
        var details = await _ordersSearchService.GetDetails(query.OrderId);

        return details;
    }
}