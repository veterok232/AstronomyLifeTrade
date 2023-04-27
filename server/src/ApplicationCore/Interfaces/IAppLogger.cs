namespace ApplicationCore.Interfaces;

public interface IAppLogger<T>
{
    void Debug(string message, params object[] args);

    void Info(string message, params object[] args);

    void Warn(string message, params object[] args);

    void Error(string message, params object[] args);

    void Exception(Exception exception, string message = "");
}