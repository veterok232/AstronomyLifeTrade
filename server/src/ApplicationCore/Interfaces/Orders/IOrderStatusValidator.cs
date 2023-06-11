using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces.Orders;

public interface IOrderStatusValidator
{
    bool IsValidForApprove(OrderStatus orderStatus);
    
    bool IsValidForPostpone(OrderStatus orderStatus);
    
    bool IsValidForCancel(OrderStatus orderStatus);
    
    bool IsValidForClose(OrderStatus orderStatus);
}