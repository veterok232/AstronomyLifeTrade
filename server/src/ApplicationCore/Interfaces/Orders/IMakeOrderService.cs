using ApplicationCore.Entities;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Orders;

namespace ApplicationCore.Interfaces.Orders;

public interface IMakeOrderService
{
    Task<Result<Order>> MakeOrder(MakeOrderModel model);
}