using ApplicationCore.Interfaces.Cart;
using ApplicationCore.Models.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.Get;

internal class GetProductsInCartQueryHandler : IRequestHandler<GetProductsInCartQuery, ICollection<Guid>>
{
    private readonly ICartService _cartService;

    public GetProductsInCartQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<ICollection<Guid>> Handle(GetProductsInCartQuery query, CancellationToken cancellationToken)
    {
        return await _cartService.GetProductsInCart();
    }
}