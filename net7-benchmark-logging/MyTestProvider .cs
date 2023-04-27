using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace net7_benchmark_logging;

class MyTestProvider : ILoggerProvider
{
    public static ILogger<T> CreateLogger<T>(LogLevel minLevel)
    {
        var filters = new LoggerFilterOptions();
        filters.AddFilter(typeof(T).FullName, minLevel);

        var factory = new LoggerFactory(new[] { new MyTestProvider() }, filters);

        return factory.CreateLogger<T>();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new MyTestLogger();
    }

    public void Dispose()
    {
    }

    private class MyTestLogger : ILogger
    {
        private IExternalScopeProvider? ScopeProvider { get; set; }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull =>
            ScopeProvider?.Push(state) ?? NoopScope.Instance;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            Debug.WriteLine("logging...");
        }
    }

    private class NoopScope : IDisposable
    {
        public static NoopScope Instance { get; } = new();

        public void Dispose()
        {
        }
    }
}