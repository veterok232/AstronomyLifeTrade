using System.ComponentModel;
using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Specifications.Common;
using ApplicationCore.Specifications.ImplicitFilters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal static class SpecificationQueryHelper<T>
    where T : Entity
{
    public static async Task<IQueryable<T>> BuildQuery(
        IQueryable<T> inputQuery,
        ISpecification<T> specification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        var query = specification.IsReadOnly ? inputQuery.AsNoTracking() : inputQuery;
        query = await ApplyBaseQuery(query, specification, dataFilters);
        query = ApplyIncludes(query, specification);
        query = ApplyOrdering(query, specification);
        query = ApplyPaging(query, specification);
        query = await ApplyOutOfPagingCriteria(inputQuery, query, specification, dataFilters);
        query = MarkAsSplitQuery(query, specification);

        return query;
    }

    public static async Task<IQueryable<TResult>> BuildDataTransformQuery<TResult>(
        IQueryable<T> inputQuery,
        IDataTransformSpecification<T, TResult> dataTransformSpecification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        IQueryable<T> query = await BuildQuery(inputQuery, dataTransformSpecification, dataFilters);
        IQueryable<TResult> transformQuery = ApplySelector(query, dataTransformSpecification);

        return ApplyDistinct(transformQuery, dataTransformSpecification);
    }

    public static async Task<IQueryable<TResult>> BuildDataGroupQuery<TResult, TKey>(
        IQueryable<T> inputQuery,
        IDataGroupingSpecification<T, TKey, TResult> dataGroupingSpecification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        IQueryable<T> query = await BuildQuery(inputQuery, dataGroupingSpecification, dataFilters);

        return ApplyGroup(query, dataGroupingSpecification);
    }

    public static Task<IQueryable<T>> BuildTotalCountQuery(
        IQueryable<T> inputQuery,
        ISpecification<T> specification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        var query = inputQuery;
        return ApplyBaseQuery(query, specification, dataFilters);
    }

    private static async Task<IQueryable<T>> ApplyImplicitFilters(
        IQueryable<T> query,
        IEnumerable<IDataFilter<T>> dataFilters,
        IEnumerable<string> skippedFiltersKeys = null)
    {
        IEnumerable<IDataFilter<T>> filtersToApply;

        if (skippedFiltersKeys.IsNullOrEmpty())
        {
            filtersToApply = dataFilters;
        }
        else if (skippedFiltersKeys.Contains(FilterKeys.All))
        {
            return query;
        }
        else
        {
            filtersToApply = dataFilters.Where(df => !skippedFiltersKeys.Contains(df.FilterKey));
        }

        foreach (var dataFilter in filtersToApply)
        {
            Expression<Func<T, bool>> predicate = await dataFilter.GetPredicate();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
        }

        return query;
    }

    private static Task<IQueryable<T>> ApplyBaseQuery(
        IQueryable<T> query,
        ISpecification<T> specification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        return ApplyImplicitFilters(query, dataFilters, specification.SkippedImplicitFiltersKeys);
    }

    private static IQueryable<T> ApplyIncludes(IQueryable<T> query, ISpecification<T> specification)
    {
        query = specification.Includes.Aggregate(
            query,
            (current, include) => include.ThenInclude == null
                ? current.Include(include.Include)
                : current.Include(include.Include).ThenInclude(include.ThenInclude));

        query = specification.IncludeStrings.Aggregate(
            query,
            (current, include) => current.Include(include));

        return query;
    }

    private static IQueryable<T> ApplyOrdering(IQueryable<T> query, ISpecification<T> specification)
    {
        if (!specification.OrderRules.IsNullOrEmpty())
        {
            var firstRule = specification.OrderRules.First();

            query = firstRule.SortOrder switch
            {
                SortOrder.Ascending => query.OrderBy(firstRule.OrderExpression),
                SortOrder.Descending => query.OrderByDescending(firstRule.OrderExpression),
                _ => throw new InvalidEnumArgumentException(
                    nameof(firstRule.SortOrder),
                    (int)firstRule.SortOrder,
                    typeof(SortOrder)),
            };

            query = specification
                .OrderRules
                .Skip(1)
                .Aggregate(query, (currentQuery, rule) =>
                    rule.SortOrder switch
                    {
                        SortOrder.Ascending => ((IOrderedQueryable<T>)currentQuery)
                            .ThenBy(rule.OrderExpression),
                        SortOrder.Descending => ((IOrderedQueryable<T>)currentQuery)
                            .ThenByDescending(rule.OrderExpression),
                        _ => throw new InvalidEnumArgumentException(
                            nameof(rule.SortOrder),
                            (int)rule.SortOrder,
                            typeof(SortOrder)),
                    });
        }

        return query;
    }

    private static IQueryable<TResult> ApplySelector<TResult>(
        IQueryable<T> query,
        IDataTransformSpecification<T, TResult> specification)
    {
        return query.Select(specification.Selector);
    }

    private static IQueryable<T> ApplyPaging(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                .Take(specification.Take);
        }

        return query;
    }

    private static IQueryable<T> MarkAsSplitQuery(IQueryable<T> query, ISpecification<T> specification)
    {
        return specification.IsSplitQuery
            ? query.AsSplitQuery()
            : query;
    }

    private static async Task<IQueryable<T>> ApplyOutOfPagingCriteria(
        IQueryable<T> inputQuery,
        IQueryable<T> query,
        ISpecification<T> specification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        if (!specification.OutOfPagingCriteria.IsNullOrEmpty())
        {
            foreach (var criterion in specification.OutOfPagingCriteria)
            {
                query = query.Union(await ApplyImplicitFilters(
                    inputQuery.Where(criterion),
                    dataFilters,
                    specification.SkippedImplicitFiltersKeys));
            }

            return ApplyOrdering(query, specification);
        }

        return query;
    }

    private static IQueryable<TResult> ApplyDistinct<TResult>(
        IQueryable<TResult> query,
        IDataTransformSpecification<T, TResult> specification)
    {
        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }

    private static IQueryable<TResult> ApplyGroup<TResult, TKey>(
        IQueryable<T> query,
        IDataGroupingSpecification<T, TKey, TResult> dataGroupingSpecification)
    {
        return query.GroupBy(dataGroupingSpecification.GroupByExpression)
            .Select(dataGroupingSpecification.GroupTransformExpression);
    }
}