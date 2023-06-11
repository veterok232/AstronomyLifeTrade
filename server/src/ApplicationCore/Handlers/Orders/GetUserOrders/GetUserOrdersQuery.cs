using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.GetUserOrders;

public record GetUserOrdersQuery(GetUserOrdersModel Model) : IRequest<SearchResult<OrderListItem>>;