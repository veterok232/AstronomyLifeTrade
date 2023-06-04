using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Orders;

namespace ApplicationCore.Interfaces.Orders;

public interface IOrdersSearchService
{
    Task<SearchResult<OrderListItem>> Search(OrdersSearchModel model);
    
    Task<OrderDetailsModel> GetDetails(Guid orderId);
}