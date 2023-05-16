using ApplicationCore.Interfaces;

namespace Infrastructure.Services;

internal class SequentialRunner : ISequentialRunner
{
    private readonly SemaphoreSlim _semaphore;

    public SequentialRunner(SemaphoreSlim semaphore)
    {
        _semaphore = semaphore;
    }

    public async Task Run(Action action)
    {
        await _semaphore.WaitAsync();
        try
        {
            action();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task Run(Func<Task> action)
    {
        await _semaphore.WaitAsync();
        try
        {
            await action();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<T> Run<T>(Func<T> action)
    {
        await _semaphore.WaitAsync();
        try
        {
            return action();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<T> Run<T>(Func<Task<T>> action)
    {
        await _semaphore.WaitAsync();
        try
        {
            return await action();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}