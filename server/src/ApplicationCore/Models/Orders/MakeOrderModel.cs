using ApplicationCore.Enums;
using ApplicationCore.Models.Cart;

namespace ApplicationCore.Models.Orders;

public class MakeOrderModel
{
    public decimal TotalAmount { get; set; }
    
    public ICollection<Guid> CartItemsIds { get; set; }

    public OrderCustomerInfo CustomerInfo { get; set; }
    
    public DeliveryType DeliveryType { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public string? CustomerNotes { get; set; }
    
    public string? PromoCode { get; set; }

    public decimal? PromoRate { get; set; }
    
    public decimal? PromoAmount { get; set; }
}