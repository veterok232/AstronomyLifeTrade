using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Orders;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Orders;

public class OrderDetailsByOrderIdSpecification : Specification<Order>
{
    public OrderDetailsByOrderIdSpecification(Guid orderId)
        : base(o => o.Id == orderId)
    {
        AddIncludes(
            o => o.ConsumerAssignment.PersonalData,
            o => o.ConsumerAssignment.PersonalData.Address,
            o => o.Address,
            o => o.OrderItems,
            o => o.ManagerAssignment.PersonalData);
        
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Comments)}");
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Category)}");
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Brand)}");
        AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Files)}");
    }
}