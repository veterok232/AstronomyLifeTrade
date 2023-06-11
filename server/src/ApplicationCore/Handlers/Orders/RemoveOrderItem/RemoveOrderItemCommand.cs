using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.RemoveOrderItem;

public record RemoveOrderItemCommand(RemoveOrderItemModel Model) : IRequest;