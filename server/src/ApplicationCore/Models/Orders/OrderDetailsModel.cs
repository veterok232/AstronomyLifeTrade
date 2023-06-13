using ApplicationCore.Entities;
using ApplicationCore.Enums;

namespace ApplicationCore.Models.Orders;

public class OrderDetailsModel
{
    public Guid Id { get; set; }
    
    public OrderStatus OrderStatus { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string CustomerFirstName { get; set; }
    
    public string CustomerLastName { get; set; }
    
    public string CustomerEmail { get; set; }
    
    public string CustomerPhoneNumber { get; set; }
    
    public int OrderNumber { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public DeliveryType DeliveryType { get; set; }
    
    public AddressModel Address { get; set; }
    
    public ICollection<OrderItemModel> OrderItems { get; set; }
    
    public string? CustomerNotes { get; set; }
    
    public string? ManagerFullName { get; set; }
    
    public string? PromoCode { get; set; }
    
    public decimal? PromoRate { get; set; }
    
    public decimal? PromoAmount { get; set; }
}