using ApplicationCore.Enums;

namespace ApplicationCore.Models.AccountProfile;

public class OrdersAggregatedDataItem
{
    public OrderStatus Status { get; set; }
    
    public int Count { get; set; }
    
    public decimal Amount { get; set; }
}