using System.Linq.Expressions;
using ApplicationCore.Extensions;

namespace ApplicationCore.Utils;

internal static class ExpressionsUtil
{
    public static Expression<Func<T, bool>> AggregateByAnd<T>(
        params Expression<Func<T, bool>>[] predicateExpressions)
    {
        if (predicateExpressions.IsNullOrEmpty())
        {
            return null;
        }

        Expression<Func<T, bool>> result = predicateExpressions.First();

        if (predicateExpressions.Length > 1)
        {
            for (int i = 1; i < predicateExpressions.Length; i++)
            {
                result = And(result, predicateExpressions[i]);
            }
        }

        return result;
    }

    public static Expression<Func<T, bool>> And<T>(
        Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        var paramExpression = Expression.Parameter(typeof(T));
        var firstExpression = ReplaceParam(first, paramExpression);
        var secondExpression = ReplaceParam(second, paramExpression);

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(firstExpression, secondExpression),
            paramExpression);
    }

    public static Expression<Func<T, bool>> Or<T>(
        Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        var paramExpression = Expression.Parameter(typeof(T));
        var firstExpression = ReplaceParam(first, paramExpression);
        var secondExpression = ReplaceParam(second, paramExpression);

        return Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(firstExpression, secondExpression),
            paramExpression);
    }

    private static Expression ReplaceParam<T>(
        Expression<Func<T, bool>> lambda,
        ParameterExpression paramExpression)
    {
        var replacer = new ParamReplaceVisitor(lambda.Parameters.First(), paramExpression);

        return replacer.Visit(lambda.Body);
    }
}