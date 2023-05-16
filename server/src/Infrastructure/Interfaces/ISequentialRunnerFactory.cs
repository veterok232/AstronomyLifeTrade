using ApplicationCore.Interfaces;

namespace Infrastructure.Interfaces;

public interface ISequentialRunnerFactory
{
    Task<ISequentialRunner> Create<TDependency>();
}