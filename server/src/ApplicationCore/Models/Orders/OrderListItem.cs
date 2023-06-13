using ApplicationCore.Enums;

namespace ApplicationCore.Models.Orders;

public class OrderListItem
{
    public Guid Id { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string CustomerFirstName { get; set; }
    
    public string CustomerLastName { get; set; }
    
    public int OrderNumber { get; set; }
    
    public string? ManagerFullName { get; set; }
}