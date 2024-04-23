using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EtAlii.Adp;

public static class ApplicationScopeLoggerExtensions
{
    public static ILoggingBuilder AddApplicationScopeLogger(this ILoggingBuilder builder, 
        Action<ApplicationScopeLoggerConfiguration> configureScope,
        Action<ILoggingBuilder> configureLogging)
    {
        var configuration = new ApplicationScopeLoggerConfiguration();
        configureScope(configuration);
        
        var factory = LoggerFactory.Create(configureLogging);
        builder.Services.AddSingleton<ILoggerFactory>(new ApplicationScopeLoggerFactory(factory, configuration.Scope));
        return builder;
    }
}