using System.Linq.Expressions;

namespace ApplicationCore.Utils;

internal class PredicateBuilder<T>
{
    private Expression<Func<T, bool>> _predicate;

    public Expression<Func<T, bool>> Build() => _predicate;

    public void And(Expression<Func<T, bool>> expressionPart)
    {
        _predicate = _predicate == null
            ? expressionPart
            : ExpressionsUtil.And(_predicate, expressionPart);
    }
}