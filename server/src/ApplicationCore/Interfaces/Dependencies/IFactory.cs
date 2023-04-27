namespace ApplicationCore.Interfaces.Dependencies;

public interface IFactory<TService>
{
    TService CreateService(object key);
}