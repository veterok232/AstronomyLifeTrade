using ApplicationCore.Models.Common;
using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.MakeOrder;

public record MakeOrderCommand(MakeOrderModel Model) : IRequest<Result<int>>;