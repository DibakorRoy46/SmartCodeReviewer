
using Microsoft.Extensions.Logging;
using Shared.Application.Abstractions;

namespace Shared.Infrastructure.Logging;

public class LoggerAdapter<T> : ILoggerAdapter<T>
{
    private readonly ILogger<T> _logger;
    public LoggerAdapter(ILogger<T> logger) => _logger = logger;

    public void LogInformation(string message) => _logger.LogInformation(message);
    public void LogWarning(string message) => _logger.LogWarning(message);
    public void LogError(string message, Exception? ex = null) => _logger.LogError(ex, message);
}