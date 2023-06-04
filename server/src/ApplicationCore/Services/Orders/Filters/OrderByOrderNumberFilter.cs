using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Orders;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Orders.Filters;

[ScopedDependency]
internal class OrderByOrderNumberFilter : IEntityFilter<Order, OrdersSearchModel>
{
    public Expression<Func<Order, bool>> GetFilterPredicate(OrdersSearchModel model)
    {
        return !model.OrderNumber.HasValue
            ? null
            : o => model.OrderNumber == o.OrderNumber;
    }
}