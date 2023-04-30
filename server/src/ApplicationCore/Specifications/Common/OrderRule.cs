using System.Linq.Expressions;
using ApplicationCore.Enums;

namespace ApplicationCore.Specifications.Common;

public class OrderRule<T>
{
    public OrderRule(SortOrder sortOrder, Expression<Func<T, object>> orderExpression)
    {
        SortOrder = sortOrder;
        OrderExpression = orderExpression;
    }

    public SortOrder SortOrder { get; set; }

    public Expression<Func<T, object>> OrderExpression { get; set; }
}