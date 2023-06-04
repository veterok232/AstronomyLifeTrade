using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Search;

internal class SearchOrdersRequestHandler : IRequestHandler<SearchOrdersRequest, SearchResult<OrderListItem>>
{
    private readonly IOrdersSearchService _ordersSearchService;

    public SearchOrdersRequestHandler(IOrdersSearchService ordersSearchService)
    {
        _ordersSearchService = ordersSearchService;
    }

    public async Task<SearchResult<OrderListItem>> Handle(
        SearchOrdersRequest request,
        CancellationToken cancellationToken)
    {
        var orders = await _ordersSearchService.Search(request.Model);

        return orders;
    }
}