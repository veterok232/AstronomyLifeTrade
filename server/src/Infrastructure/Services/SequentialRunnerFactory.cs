using System.Collections.Concurrent;
using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

[SingleDependency]
internal class SequentialRunnerFactory : ISequentialRunnerFactory
{
    private readonly IDictionary<Type, SemaphoreSlim> _semaphoreCache = new ConcurrentDictionary<Type, SemaphoreSlim>();
    private readonly SequentialRunner _sequentialRunner;

    public SequentialRunnerFactory()
    {
        _sequentialRunner = new SequentialRunner(CreateSemaphore<SequentialRunnerFactory>());
    }

    public async Task<ISequentialRunner> Create<TDependency>()
    {
        return new SequentialRunner(await GetSemaphore<TDependency>());
    }

    private async Task<SemaphoreSlim> GetSemaphore<TDependency>()
    {
        return GetSemaphoreFromCache<TDependency>() ?? await CreateSemaphoreConcurrent<TDependency>();
    }

    private Task<SemaphoreSlim> CreateSemaphoreConcurrent<TDependency>()
    {
        return _sequentialRunner.Run(CreateSemaphore<TDependency>);
    }

    private SemaphoreSlim CreateSemaphore<TDependency>()
    {
        var semaphore = GetSemaphoreFromCache<TDependency>();
        if (semaphore == null)
        {
            semaphore = new SemaphoreSlim(1, 1);
            _semaphoreCache.Add(typeof(TDependency), semaphore);
        }

        return semaphore;
    }

    private SemaphoreSlim GetSemaphoreFromCache<TDependency>()
    {
        return _semaphoreCache.TryGetValue(typeof(TDependency), out var semaphore) ? semaphore : null;
    }
}