using System.Linq.Expressions;
using AutoMapper;

namespace ApplicationCore.Extensions;

public static class AutoMapperExtension
{
    public static IMappingExpression<TSource, TDestination> IgnoreProperties<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> expression, params Expression<Func<TDestination, object>>[] propertiesToIgnore)
    {
        return propertiesToIgnore.Aggregate(expression, (current, property) => current.ForMember(property, opts => opts.Ignore()));
    }
}