using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging;

[ScopedDependency]
public class AppLogger<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public AppLogger(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<T>();

    public void Debug(string message, params object[] args) => _logger.LogDebug(message, args);

    public void Info(string message, params object[] args) => _logger.LogInformation(message, args);

    public void Warn(string message, params object[] args) => _logger.LogWarning(message, args);

    public void Error(string message, params object[] args) => _logger.LogError(message, args);

    public void Exception(Exception exception, string message = "") => _logger.LogError(exception, message: message);
}