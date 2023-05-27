using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.Clear;

internal class ClearCartCommandHandler : AsyncRequestHandler<ClearCartCommand>
{
    private readonly ICartService _cartService;
    private readonly IUnitOfWork _unitOfWork;

    public ClearCartCommandHandler(
        ICartService cartService,
        IUnitOfWork unitOfWork)
    {
        _cartService = cartService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(ClearCartCommand command, CancellationToken cancellationToken)
    {
        await _cartService.Clear();
        await _unitOfWork.Commit();
    }
}