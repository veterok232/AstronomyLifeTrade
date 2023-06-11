using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.GetUserOrders;

internal class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, SearchResult<OrderListItem>>
{
    private readonly IOrdersSearchService _ordersSearchService;

    public GetUserOrdersQueryHandler(IOrdersSearchService ordersSearchService)
    {
        _ordersSearchService = ordersSearchService;
    }

    public async Task<SearchResult<OrderListItem>> Handle(
        GetUserOrdersQuery query,
        CancellationToken cancellationToken)
    {
        var orders = await _ordersSearchService.SearchUserOrders(query.Model);

        return orders;
    }
}