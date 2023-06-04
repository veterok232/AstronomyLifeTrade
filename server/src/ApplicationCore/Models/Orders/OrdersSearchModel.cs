using ApplicationCore.Enums;
using ApplicationCore.Handlers.Common;

namespace ApplicationCore.Models.Orders;

public class OrdersSearchModel : ISortable, IPageable
{
    public ICollection<OrderStatus>? OrderStatuses { get; set; }
    
    public int? OrderNumber { get; set; }
    
    public string SortBy { get; set; }
    
    public SortOrder Direction  { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize  { get; set; }
}