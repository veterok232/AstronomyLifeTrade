using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Handlers.Orders.Actions;
using ApplicationCore.Handlers.Orders.Details;
using ApplicationCore.Handlers.Orders.GetCustomerInfo;
using ApplicationCore.Handlers.Orders.MakeOrder;
using ApplicationCore.Handlers.Orders.Search;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Routes.Orders.Search)]
    [Authorization(Roles.Manager, Roles.Staff)]
    public Task<SearchResult<OrderListItem>> Search([FromQuery] OrdersSearchModel model)
    {
        return _mediator.Send(new SearchOrdersRequest(model));
    }
    
    [HttpGet(Routes.Orders.Details)]
    [Authorization(Roles.Manager, Roles.Staff)]
    public Task<OrderDetailsModel> Details(Guid orderId)
    {
        return _mediator.Send(new GetOrderDetailsModelQuery(orderId));
    }
    
    [HttpGet(Routes.Orders.GetCustomerInfo)]
    [Authorization(Roles.Consumer)]
    public Task<OrderCustomerInfo> GetCustomerInfo()
    {
        return _mediator.Send(new GetOrderCustomerInfoQuery());
    }
    
    [HttpPost(Routes.Orders.MakeOrder)]
    [Authorization(Roles.Consumer)]
    public Task<Result<int>> MakeOrder([FromBody] MakeOrderModel data)
    {
        return _mediator.Send(new MakeOrderCommand(data));
    }
    
    [HttpPost(Routes.Orders.PostponeOrder)]
    [Authorization(Roles.Manager, Roles.Staff)]
    public Task PostponeOrder(Guid orderId)
    {
        return _mediator.Send(new PostponeOrderCommand(orderId));
    }
    
    [HttpPost(Routes.Orders.CancelOrder)]
    [Authorization(Roles.Manager, Roles.Staff)]
    public Task CancelOrder(Guid orderId)
    {
        return _mediator.Send(new CancelOrderCommand(orderId));
    }
    
    [HttpPost(Routes.Orders.ApproveOrder)]
    [Authorization(Roles.Manager, Roles.Staff)]
    public Task ApproveOrder(Guid orderId)
    {
        return _mediator.Send(new ApproveOrderCommand(orderId));
    }
    
    [HttpPost(Routes.Orders.CloseOrder)]
    [Authorization(Roles.Manager, Roles.Staff)]
    public Task CloseOrder(Guid orderId)
    {
        return _mediator.Send(new CloseOrderCommand(orderId));
    }
}