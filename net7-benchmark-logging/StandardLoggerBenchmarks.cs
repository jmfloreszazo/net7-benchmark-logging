using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace net7_benchmark_logging;

[MemoryDiagnoser]
public class StandardLoggerBenchmarks
{
    private static readonly ILogger<StandardLoggerBenchmarks> Logger =
        MyTestProvider.CreateLogger<StandardLoggerBenchmarks>(LogLevel.Warning);

    private static readonly SampleValue SomeValue = new("Some value");

    [Benchmark]
    public void EnabledLogWarning()
    {
        Logger.LogWarning("{SomeValueOne}{SomeValueTwo}", 10, SomeValue);
    }

    [Benchmark]
    public void DisabledLogInformation()
    {
        Logger.LogInformation("{SomeValueOne}{SomeValueTwo}", 10, SomeValue);
    }

    [Benchmark]
    public void EnabledSourceGeneratedLogWarning()
    {
        Logger.LogW(10, SomeValue);
    }

    [Benchmark]
    public void DisabledSourceGeneratedLogInfo()
    {
        Logger.LogI(10, SomeValue);
    }
}

internal static partial class Log
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "{SomeValueOne} {SomeValueTwo}")]
    public static partial void LogI(this ILogger<StandardLoggerBenchmarks> logger, int someValueOne, SampleValue someValueTwo);

    [LoggerMessage(EventId = 2, Level = LogLevel.Warning, Message = "{SomeValueOne} {SomeValueTwo}")]
    public static partial void LogW(this ILogger<StandardLoggerBenchmarks> logger, int someValueOne, SampleValue someValueTwo);
}