using System.Linq.Expressions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Utils;

namespace ApplicationCore.Services.Search;

[ScopedDependency]
internal class EntityFilterQueryBuilder<TEntity, TFilterModel> : IEntityFilterQueryBuilder<TEntity, TFilterModel>
{
    private readonly IEnumerable<IEntityFilter<TEntity, TFilterModel>> _filters;

    public EntityFilterQueryBuilder(IEnumerable<IEntityFilter<TEntity, TFilterModel>> filters)
    {
        _filters = filters;
    }

    public Expression<Func<TEntity, bool>> BuildQuery(TFilterModel model)
    {
        var builder = new PredicateBuilder<TEntity>();
        _filters.Select(f => f.GetFilterPredicate(model))
            .Where(e => e != null)
            .ForEach(builder.And);

        return builder.Build();
    }
}