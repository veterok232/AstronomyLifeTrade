using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.Cart.Add;
using ApplicationCore.Handlers.Cart.ChangeCount;
using ApplicationCore.Handlers.Cart.Get;
using ApplicationCore.Handlers.Cart.Remove;
using ApplicationCore.Models.Cart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.Cart.Get)]
    [Authorization(Roles.Consumer)]
    public Task<CartModel> Get()
    {
        return _mediator.Send(new GetCartQuery());
    }
    
    [HttpGet(Routes.Cart.GetProductInCart)]
    [Authorization(Roles.Consumer)]
    public Task<ICollection<Guid>> GetProductsInCart()
    {
        return _mediator.Send(new GetProductsInCartQuery());
    }
    
    [HttpPost(Routes.Cart.Add)]
    [Authorization(Roles.Consumer)]
    public Task Add(AddProductToCartModel model)
    {
        return _mediator.Send(new AddProductToCartCommand(model.ProductId));
    }
    
    [HttpPost(Routes.Cart.Remove)]
    [Authorization(Roles.Consumer)]
    public Task Remove(AddProductToCartModel model)
    {
        return _mediator.Send(new RemoveProductFromCartCommand(model.ProductId));
    }
    
    [HttpPost(Routes.Cart.ChangeCount)]
    [Authorization(Roles.Consumer)]
    public Task ChangeCount(CartChangeCountModel model)
    {
        return _mediator.Send(new CartChangeCountCommand(model));
    }
}