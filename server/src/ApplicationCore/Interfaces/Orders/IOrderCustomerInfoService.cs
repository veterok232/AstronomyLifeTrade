using ApplicationCore.Models.Orders;

namespace ApplicationCore.Interfaces.Orders;

public interface IOrderCustomerInfoService
{
    Task<OrderCustomerInfo> Get();
}