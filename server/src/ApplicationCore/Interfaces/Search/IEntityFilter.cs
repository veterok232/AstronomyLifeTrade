using System.Linq.Expressions;

namespace ApplicationCore.Interfaces.Search;

internal interface IEntityFilter<TEntity, in TFilterModel>
{
    Expression<Func<TEntity, bool>> GetFilterPredicate(TFilterModel model);
}