namespace ApplicationCore.Interfaces.Orders;

public interface IOrdersService
{
    Task PostponeOrder(Guid orderId);
    
    Task CancelOrder(Guid orderId);
    
    Task ApproveOrder(Guid orderId);
    
    Task CloseOrder(Guid orderId);
}