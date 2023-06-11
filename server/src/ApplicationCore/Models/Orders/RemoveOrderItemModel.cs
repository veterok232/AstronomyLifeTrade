namespace ApplicationCore.Models.Orders;

public class RemoveOrderItemModel
{
    public Guid OrderId { get; set; }
    
    public Guid OrderItemId { get; set; }
}