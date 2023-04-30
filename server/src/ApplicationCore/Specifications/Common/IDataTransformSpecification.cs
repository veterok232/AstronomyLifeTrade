using System.Linq.Expressions;
using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.Common;

public interface IDataTransformSpecification<TSource, TResult> : ISpecification<TSource>
    where TSource : Entity
{
    Expression<Func<TSource, TResult>> Selector { get; }

    bool IsDistinct { get; }
}