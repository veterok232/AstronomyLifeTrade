using ApplicationCore.Models.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.ChangeCount;

public record CartChangeCountCommand(CartChangeCountModel Model) : IRequest;