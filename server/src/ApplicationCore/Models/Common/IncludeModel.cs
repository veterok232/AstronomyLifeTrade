using System.Linq.Expressions;

namespace ApplicationCore.Models.Common;

public class IncludeModel<T, TProperty>
{
    public Expression<Func<T, TProperty>> Include { get; set; }

    public Expression<Func<TProperty, object>> ThenInclude { get; set; }
}