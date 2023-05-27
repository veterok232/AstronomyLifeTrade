using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Handlers.Orders;
using ApplicationCore.Handlers.Orders.GetCustomerInfo;
using ApplicationCore.Handlers.Orders.MakeOrder;
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
    
    [HttpGet(Routes.Orders.GetCustomerInfo)]
    [Authorization(Roles.Consumer)]
    public Task<OrderCustomerInfo> GetOrderCustomerInfo()
    {
        return _mediator.Send(new GetOrderCustomerInfoQuery());
    }
    
    [HttpPost(Routes.Orders.MakeOrder)]
    [Authorization(Roles.Consumer)]
    public Task<Result<int>> MakeOrder([FromBody] MakeOrderModel data)
    {
        return _mediator.Send(new MakeOrderCommand(data));
    }
}