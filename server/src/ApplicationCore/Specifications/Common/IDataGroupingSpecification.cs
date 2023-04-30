using System.Linq.Expressions;
using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.Common;

public interface IDataGroupingSpecification<TSource, TKey, TResult> : ISpecification<TSource>
    where TSource : Entity
{
    Expression<Func<TSource, TKey>> GroupByExpression { get; }

    Expression<Func<IGrouping<TKey, TSource>, TResult>> GroupTransformExpression { get; }
}