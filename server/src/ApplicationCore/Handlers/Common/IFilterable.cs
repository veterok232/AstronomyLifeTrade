using System.Linq.Expressions;
using ApplicationCore.Entities;

namespace ApplicationCore.Handlers.Common;

public interface IFilterable<T>
    where T : Entity
{
    Expression<Func<T, bool>> GetFilterPredicate();
}