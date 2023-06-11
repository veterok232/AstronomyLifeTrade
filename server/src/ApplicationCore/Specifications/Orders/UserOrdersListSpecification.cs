using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Orders;

namespace ApplicationCore.Specifications.Orders;

internal class UserOrdersListSpecification : OrdersListBaseSpecification
{
    public UserOrdersListSpecification(OrdersSearchData ordersSearchData, Guid consumerAssignmentId)
        : base(o => o.ConsumerAssignmentId == consumerAssignmentId)
    {
        ApplyOrderBy(ordersSearchData);

        ApplyPaging(ordersSearchData.PageNumber, ordersSearchData.PageSize);
    }

    protected override Dictionary<string, Expression<Func<Order, object>>> OrderByOptions =>
        new()
        {
            { "OrderNumber", o => o.OrderNumber },
        };
}