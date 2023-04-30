using System.Linq.Expressions;
using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.Common;

public class DataTransformSpecification<TSource, TResult>
    : Specification<TSource>, IDataTransformSpecification<TSource, TResult>
    where TSource : Entity
{
    public DataTransformSpecification(
        Expression<Func<TSource, TResult>> selector,
        Expression<Func<TSource, bool>> criteria = null)
        : base(criteria)
    {
        Selector = selector ?? throw new ArgumentNullException(nameof(selector));
    }

    public Expression<Func<TSource, TResult>> Selector { get; }

    public bool IsDistinct { get; private set; }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
}