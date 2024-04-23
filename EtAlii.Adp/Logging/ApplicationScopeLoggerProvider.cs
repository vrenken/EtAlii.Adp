using Microsoft.Extensions.Logging;

namespace EtAlii.Adp;

public class ApplicationScopeLoggerFactory : ILoggerFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly Dictionary<string, object> _scope;

    public ApplicationScopeLoggerFactory(ILoggerFactory loggerFactory, Dictionary<string, string> scope)
    {
        _loggerFactory = loggerFactory;
        _scope = scope
            .Select(kvp => new KeyValuePair<string,object>(kvp.Key, kvp.Value))
            .ToDictionary();
    }

    public void Dispose() => _loggerFactory.Dispose();

    public void AddProvider(ILoggerProvider provider) => _loggerFactory.AddProvider(provider);

    public ILogger CreateLogger(string categoryName) => new ApplicationScopeLogger(_loggerFactory.CreateLogger(categoryName), _scope);
    
}