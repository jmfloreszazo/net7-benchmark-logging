using BenchmarkDotNet.Running;
using net7_benchmark_logging;

//dotnet run -c Release -f net7.0

BenchmarkRunner.Run(new[] {
    typeof(StandardLoggerBenchmarks),
    typeof(OptimizedLoggerBenchmarks)});