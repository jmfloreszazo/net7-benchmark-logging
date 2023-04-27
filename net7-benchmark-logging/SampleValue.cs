namespace net7_benchmark_logging;

internal class SampleValue
{
    private readonly string _value;

    public SampleValue(string value)
    {
        _value = value;
    }

    public override string ToString()
    {
        return _value[3..];
    }
}