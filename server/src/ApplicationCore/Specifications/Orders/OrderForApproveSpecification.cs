using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Orders;

public class OrderForApproveSpecification : Specification<Order>
{
    public OrderForApproveSpecification(Guid orderId)
        : base(o => o.Id == orderId)
    {
        AddIncludes(
            o => o.ConsumerAssignment.PersonalData,
            o => o.ConsumerAssignment.PersonalData.Address,
            o => o.Address,
            o => o.OrderItems);
        
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Comments)}");
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Category)}");
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Brand)}");
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Files)}");
    }
}