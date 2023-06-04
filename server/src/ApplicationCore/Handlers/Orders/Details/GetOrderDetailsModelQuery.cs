using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.Details;

public record GetOrderDetailsModelQuery(Guid OrderId) : IRequest<OrderDetailsModel>;