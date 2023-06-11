using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Orders;

[ScopedDependency]
public class OrderStatusValidator : IOrderStatusValidator
{
    private static readonly List<OrderStatus> ValidOrderStatusesForApprove = new()
    {
        OrderStatus.Pending,
        OrderStatus.Postponed,
    };
    
    private static readonly List<OrderStatus> ValidOrderStatusesForPostpone = new()
    {
        OrderStatus.Pending,
    };
    
    private static readonly List<OrderStatus> ValidOrderStatusesForCancel = new()
    {
        OrderStatus.Pending,
        OrderStatus.Postponed,
        OrderStatus.Approved,
    };
    
    private static readonly List<OrderStatus> ValidOrderStatusesForClose = new()
    {
        OrderStatus.Approved,
    };

    public bool IsValidForApprove(OrderStatus orderStatus)
    {
        return ValidOrderStatusesForApprove.Contains(orderStatus);
    }

    public bool IsValidForPostpone(OrderStatus orderStatus)
    {
        return ValidOrderStatusesForPostpone.Contains(orderStatus);
    }

    public bool IsValidForCancel(OrderStatus orderStatus)
    {
        return ValidOrderStatusesForCancel.Contains(orderStatus);
    }

    public bool IsValidForClose(OrderStatus orderStatus)
    {
        return ValidOrderStatusesForClose.Contains(orderStatus);
    }
}