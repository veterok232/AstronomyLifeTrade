using ApplicationCore.Interfaces.Cart;
using ApplicationCore.Models.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.Get;

internal class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartModel>
{
    private readonly ICartService _cartService;

    public GetCartQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartModel> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        return await _cartService.Get();
    }
}