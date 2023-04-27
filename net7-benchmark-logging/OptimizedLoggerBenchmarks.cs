using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace net7_benchmark_logging;

[MemoryDiagnoser]
public class OptimizedLoggerBenchmarks
{
    private static readonly ILogger<OptimizedLoggerBenchmarks> Logger =
        MyTestProvider.CreateLogger<OptimizedLoggerBenchmarks>(LogLevel.Warning);

    private static readonly Action<ILogger, int, SampleValue, Exception> LogWarn =
        LoggerMessage.Define<int, SampleValue>(LogLevel.Warning, new EventId(3), "{SampleValueOne} {SampleValueTwo}");

    private static readonly Action<ILogger, int, SampleValue, Exception> LogInfo =
        LoggerMessage.Define<int, SampleValue>(LogLevel.Information, new EventId(4), "{SampleValueOne} {SampleValueTwo}");

    private static readonly SampleValue SomeValue = new("Some value");

    [Benchmark]
    public void EnabledLogWarning()
    {
        Logger.LogWarning("{SampleValueOne}{SampleValueTwo}", 10, SomeValue);
    }

    [Benchmark]
    public void DisabledLogInformation()
    {
        Logger.LogInformation("{SampleValueOne}{SampleValueTwo}", 10, SomeValue);
    }

    [Benchmark]
    public void EnabledHighPerfLogWarn()
    {
        LogWarn(Logger, 10, SomeValue, default!);
    }

    [Benchmark]
    public void DisabledHighPerfLogInfo()
    {
        LogInfo(Logger, 10, SomeValue, default!);
    }
}