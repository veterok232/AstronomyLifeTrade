using System.Linq.Expressions;

namespace ApplicationCore.Interfaces.Search;

public interface IEntityFilterQueryBuilder<TEntity, in TFilterModel>
{
    Expression<Func<TEntity, bool>> BuildQuery(TFilterModel model);
}