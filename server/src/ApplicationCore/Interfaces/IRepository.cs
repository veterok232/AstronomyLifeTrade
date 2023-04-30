using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Interfaces;

public interface IRepository<T>
    where T : Entity
{
    Task<T> GetSingleOrDefault(ISpecification<T> specification);

    Task<TResult> GetSingleOrDefault<TResult>(IDataTransformSpecification<T, TResult> specification);

    Task<T> GetById(Guid id);

    Task<IReadOnlyList<T>> ListAll();

    Task<IReadOnlyList<T>> List(ISpecification<T> specification);

    Task<IReadOnlyList<TResult>> List<TResult>(IDataTransformSpecification<T, TResult> dataTransformSpecification);

    Task<IReadOnlyList<TResult>> List<TKey, TResult>(
        IDataGroupingSpecification<T, TKey, TResult> dataGroupingSpecification);

    Task<T> Add(T entity);

    Task Add(T[] entities);

    Task<T> Update(T entity);

    Task Update(IEnumerable<T> entities);

    Task DeleteById(Guid id);

    Task Delete(T entity);

    Task DeleteIfExists(T entity);

    Task DeleteIfExists(ISpecification<T> specification);

    Task DeleteSingleIfExists(ISpecification<T> specification);

    Task DeleteSingle(ISpecification<T> specification);

    Task DeleteRange(IEnumerable<T> entities);

    Task DeleteRange(ISpecification<T> specification);

    Task<int> Count(ISpecification<T> specification);

    Task<int> Count();

    Task<bool> Any(ISpecification<T> specification);

    Task<bool> Any();

    Task<SearchResult<T>> Search(ISpecification<T> specification);

    Task<SearchResult<TResult>> Search<TResult>(IDataTransformSpecification<T, TResult> specification);

    Task<decimal> Sum(IDataTransformSpecification<T, decimal> dataTransformSpecification);

    Task<decimal?> Average(IDataTransformSpecification<T, decimal?> dataTransformSpecification);

    Task<TResult?> Max<TResult>(IDataTransformSpecification<T, TResult?> dataTransformSpecification)
        where TResult : struct;
}