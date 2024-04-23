using Microsoft.Extensions.Logging;

namespace EtAlii.Adp;

public class ApplicationScopeLogger : ILogger
{
    private readonly ILogger _logger;
    private readonly Dictionary<string, object> _scope;

    public ApplicationScopeLogger(ILogger logger, Dictionary<string, object> scope)
    {
        _logger = logger;
        _scope = scope;
    }
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return _logger.BeginScope(state);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return _logger.IsEnabled(logLevel);
    }

    public void Log<TState>(
        LogLevel logLevel, 
        EventId eventId, 
        TState state, 
        Exception? exception, 
        Func<TState, Exception?, string> formatter)
    {
        using var _ = _logger.BeginScope(_scope);
        _logger.Log(logLevel, eventId, state, exception, formatter);
    }
}