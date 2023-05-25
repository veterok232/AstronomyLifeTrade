using System.Linq.Expressions;

namespace ApplicationCore.Interfaces.Search;

internal interface IEntityFilterQueryBuilder<TEntity, in TFilterModel>
{
    Expression<Func<TEntity, bool>> BuildQuery(TFilterModel model);
}