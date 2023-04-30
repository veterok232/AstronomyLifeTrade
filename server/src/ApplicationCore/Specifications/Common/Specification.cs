using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Common;

namespace ApplicationCore.Specifications.Common;

public class Specification<T> : ISpecification<T>
    where T : Entity
{
    private const int MaxPageSize = 200;

    public Specification()
    {
    }

    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public virtual List<OrderRule<T>> OrderRules { get; } = new();

    public virtual List<string> SkippedImplicitFiltersKeys { get; } = new();

    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, bool>>> OutOfPagingCriteria { get; } = new();

    public List<IncludeModel<T, object>> Includes { get; } = new();

    public List<string> IncludeStrings { get; } = new();

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public bool IsReadOnly { get; private set; }

    public bool TreatEmptyResultAsConcurrency { get; private set; }

    public bool IsSplitQuery { get; private set; }

    protected virtual Dictionary<string, Expression<Func<T, object>>> OrderByOptions { get; }

    protected void AddIncludes(params Expression<Func<T, object>>[] includes)
    {
        if (!includes.IsNullOrEmpty())
        {
            Includes.AddRange(includes.Select(i => new IncludeModel<T, object> { Include = i }));
        }
    }

    protected void AddInclude(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude = null)
    {
        Includes.Add(new IncludeModel<T, object>
        {
            Include = include,
            ThenInclude = thenInclude,
        });
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        AddOrderRule(SortOrder.Descending, orderByDescendingExpression);
    }

    /// <summary>
    ///     Assigns order by rule for specification based on <see cref="ISortable"/> properties.
    /// </summary>
    /// <param name="sortableData">The data which stores order by parameters(Direction and Property to order by) <see cref="ISortable"/>.</param>
    protected void ApplyOrderBy(ISortable sortableData)
    {
        if (string.IsNullOrWhiteSpace(sortableData.SortBy))
        {
            return;
        }

        var hasOrderByRuleDefined =
            OrderByOptions.TryGetValue(sortableData.SortBy, out Expression<Func<T, object>> orderByExpression);

        if (!hasOrderByRuleDefined)
        {
            return;
        }

        if (sortableData.Direction == SortOrder.Ascending)
        {
            ApplyOrderBy(orderByExpression);
        }
        else
        {
            ApplyOrderByDescending(orderByExpression);
        }
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        AddOrderRule(SortOrder.Ascending, orderByExpression);
    }

    protected void AddOrderRule(SortOrder sortOrder, Expression<Func<T, object>> orderingExpression)
    {
        OrderRules.Add(new OrderRule<T>(sortOrder, orderingExpression));
    }

    protected void ApplyPaging(int pageNumber, int pageSize)
    {
        if (pageSize > MaxPageSize)
        {
            throw new ArgumentOutOfRangeException(
                nameof(pageSize),
                $"Size of requested page ({pageSize}) exceeds maximum value of {MaxPageSize} items");
        }

        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
        IsPagingEnabled = true;
    }

    protected void MarkAsReadOnly()
    {
        IsReadOnly = true;
    }

    protected void ApplyTreatingEmptyResultAsConcurrency()
    {
        TreatEmptyResultAsConcurrency = true;
    }

    protected void SkipImplicitFilters(params string[] filters)
    {
        SkippedImplicitFiltersKeys.AddRange(filters);
    }

    protected void AddOutOfPagingCriteria(Expression<Func<T, bool>> criteria)
    {
        OutOfPagingCriteria.Add(criteria);
    }

    protected void MarkAsSplitQuery()
    {
        IsSplitQuery = true;
    }
}