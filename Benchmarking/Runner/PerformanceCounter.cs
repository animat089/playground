using BenchmarkDotNet.Running;
using Iterations;

public class PerformanceCounter
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<IterationSamples>();
    }
}
