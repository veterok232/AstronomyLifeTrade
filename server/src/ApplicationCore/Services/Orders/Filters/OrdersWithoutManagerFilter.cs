using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Orders;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Orders.Filters;

[ScopedDependency]
internal class OrdersWithoutManagerFilter : IEntityFilter<Order, OrdersSearchModel>
{
    public Expression<Func<Order, bool>> GetFilterPredicate(OrdersSearchModel model)
    {
        return model.IsWithoutManager == true
            ? o => !o.ManagerAssignmentId.HasValue
            : null;
    }
}