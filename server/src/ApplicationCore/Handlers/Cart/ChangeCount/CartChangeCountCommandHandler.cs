using ApplicationCore.Handlers.Cart.Remove;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.ChangeCount;

internal class CartChangeCountCommandHandler : AsyncRequestHandler<CartChangeCountCommand>
{
    private readonly ICartService _cartService;
    private readonly IUnitOfWork _unitOfWork;

    public CartChangeCountCommandHandler(
        ICartService cartService,
        IUnitOfWork unitOfWork)
    {
        _cartService = cartService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(CartChangeCountCommand command, CancellationToken cancellationToken)
    {
        await _cartService.ChangeProductCount(command.Model);
        await _unitOfWork.Commit();
    }
}