using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Orders;

namespace ApplicationCore.Specifications.Orders;

internal class OrdersListSpecification : OrdersListBaseSpecification
{
    public OrdersListSpecification(OrdersSearchData ordersSearchData)
        : base(ordersSearchData.FilterPredicate)
    {
        ApplyOrderBy(ordersSearchData);

        ApplyPaging(ordersSearchData.PageNumber, ordersSearchData.PageSize);
    }

    protected override Dictionary<string, Expression<Func<Order, object>>> OrderByOptions =>
        new()
        {
            { "CreatedAt", o => o.CreatedAt },
            { "TotalAmount", o => o.TotalAmount },
        };
}