using ApplicationCore.Models.Common;
using ApplicationCore.Models.Orders;

namespace ApplicationCore.Interfaces.Orders;

public interface IOrdersService
{
    Task PostponeOrder(Guid orderId);
    
    Task CancelOrder(Guid orderId);
    
    Task<Result> ApproveOrder(Guid orderId);
    
    Task CloseOrder(Guid orderId);

    Task RemoveOrderItem(RemoveOrderItemModel model);
}