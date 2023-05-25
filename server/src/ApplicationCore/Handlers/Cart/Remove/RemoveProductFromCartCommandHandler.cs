using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.Remove;

internal class RemoveProductFromCartCommandHandler : AsyncRequestHandler<RemoveProductFromCartCommand>
{
    private readonly ICartService _cartService;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProductFromCartCommandHandler(
        ICartService cartService,
        IUnitOfWork unitOfWork)
    {
        _cartService = cartService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(RemoveProductFromCartCommand command, CancellationToken cancellationToken)
    {
        await _cartService.Remove(command.ProductId);
        await _unitOfWork.Commit();
    }
}