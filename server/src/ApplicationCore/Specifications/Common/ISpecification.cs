using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Common;

namespace ApplicationCore.Specifications.Common;

public interface ISpecification<T>
    where T : Entity
{
    List<string> SkippedImplicitFiltersKeys { get; }

    Expression<Func<T, bool>> Criteria { get; }

    List<Expression<Func<T, bool>>> OutOfPagingCriteria { get; }

    List<IncludeModel<T, object>> Includes { get; }

    List<string> IncludeStrings { get; }

    List<OrderRule<T>> OrderRules { get; }

    int Take { get; }

    int Skip { get; }

    bool IsPagingEnabled { get; }

    bool IsReadOnly { get; }

    bool TreatEmptyResultAsConcurrency { get; }

    bool IsSplitQuery { get; }
}