namespace ApplicationCore.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}