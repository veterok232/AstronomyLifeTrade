namespace ApplicationCore.Interfaces;

public interface ISequentialRunner
{
    Task Run(Action action);

    Task Run(Func<Task> action);

    Task<T> Run<T>(Func<T> action);

    Task<T> Run<T>(Func<Task<T>> action);
}