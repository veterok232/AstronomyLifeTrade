using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Common;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Utils;

namespace ApplicationCore.Services.Orders;

[ScopedDependency]
public class OrderActionsValidator : IOrderActionsValidator
{
    public Result IsValidForApprove(Order order)
    {
        foreach (var orderItem in order.OrderItems)
        {
            if (orderItem.Quantity > orderItem.Product.Quantity)
            {
                return ResultBuilder.BuildFailed<bool>("Данных товаров нет в наличии");
            }
        }

        return ResultBuilder.BuildSucceeded();
    }
}