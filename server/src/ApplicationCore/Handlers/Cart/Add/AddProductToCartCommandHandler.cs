using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.Add;

internal class AddProductToCartCommandHandler : AsyncRequestHandler<AddProductToCartCommand>
{
    private readonly ICartService _cartService;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductToCartCommandHandler(
        ICartService cartService,
        IUnitOfWork unitOfWork)
    {
        _cartService = cartService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(AddProductToCartCommand command, CancellationToken cancellationToken)
    {
        await _cartService.Add(command.ProductId);
        await _unitOfWork.Commit();
    }
}