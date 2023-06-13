using System.Linq.Expressions;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Specifications.Common;
using ApplicationCore.Utils;

namespace ApplicationCore.Specifications.AccountProfile;

public class OrdersForStatisticsSpecification : Specification<Order>
{
    public OrdersForStatisticsSpecification(StatisticsQuery query)
        : base(GetSelectExpression(query))
    {
    }
    
    private static Expression<Func<Order, bool>> GetSelectExpression(StatisticsQuery query)
    {
        var builder = new PredicateBuilder<Order>();

        if (query.ManagerAssignmentId.HasValue)
        {
            builder.And(o => o.ManagerAssignmentId == query.ManagerAssignmentId);
        }

        builder.And(o => o.CreatedAt >= query.CreatedOnFrom && o.CreatedAt <= query.CreatedOnTo);

        return builder.Build();
    }
}