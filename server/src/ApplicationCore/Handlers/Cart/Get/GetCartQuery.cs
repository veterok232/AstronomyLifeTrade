using ApplicationCore.Models.Cart;
using MediatR;

namespace ApplicationCore.Handlers.Cart.Get;

public record GetCartQuery() : IRequest<CartModel>;