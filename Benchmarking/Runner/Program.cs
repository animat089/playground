using BenchmarkDotNet.Running;
using Benchmarking.Iterations;
using Benchmarking.Mapping;

public class Program
{
    public static void Main(string[] args)
    {
        //BenchmarkRunner.Run<IterationSamples>();

        BenchmarkRunner.Run<MappingSamples>();
    }
}
