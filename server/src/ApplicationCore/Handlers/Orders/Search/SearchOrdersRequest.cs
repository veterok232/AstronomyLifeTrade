using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Search;

public record SearchOrdersRequest(OrdersSearchModel Model) : IRequest<SearchResult<OrderListItem>>;