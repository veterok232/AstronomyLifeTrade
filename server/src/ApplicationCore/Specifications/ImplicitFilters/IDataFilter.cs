using System.Linq.Expressions;

namespace ApplicationCore.Specifications.ImplicitFilters;

public interface IDataFilter<T>
{
    string FilterKey { get; }

    Task<Expression<Func<T, bool>>> GetPredicate();
}