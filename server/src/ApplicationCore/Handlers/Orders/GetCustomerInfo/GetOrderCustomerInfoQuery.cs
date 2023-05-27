using ApplicationCore.Models.Orders;
using MediatR;

namespace ApplicationCore.Handlers.Orders.GetCustomerInfo;

public record GetOrderCustomerInfoQuery : IRequest<OrderCustomerInfo>;