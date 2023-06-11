using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Orders;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Orders;

internal abstract class OrdersListBaseSpecification : DataTransformSpecification<Order, OrderListItem>
{
    protected OrdersListBaseSpecification(
        Expression<Func<Order, bool>> criteria)
        : base(
            o => new OrderListItem
            {
                Id = o.Id,
                Status = o.OrderStatus,
                TotalAmount = o.TotalAmount,
                Quantity = o.OrderItems.Count,
                CreatedAt = o.CreatedAt,
                CustomerFirstName = o.FirstName,
                CustomerLastName = o.LastName,
                OrderNumber = o.OrderNumber
            },
            criteria)
    {
        AddIncludes(
            o => o.ConsumerAssignment.PersonalData,
            o => o.OrderItems);
    }
}