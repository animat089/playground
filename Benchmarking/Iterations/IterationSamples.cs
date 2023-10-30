using BenchmarkDotNet.Attributes;
using BenchMarking.Iterations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Benchmarking.Iterations;

[MemoryDiagnoser(false)]
public class IterationSamples
{
    private int[] sampleSetArray;
    private List<int> sampleSetList;
    private IEnumerable<int> sampleSetEnumerable;

    [Params(10, 1_000, 1_00_000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void SetupData()
    {
        var sampleSet = ListGenerator.GenerateList(Size);

        sampleSetArray = sampleSet.ToArray();
        sampleSetList = sampleSet.ToList();
        sampleSetEnumerable = sampleSet;
    }

    [Benchmark]
    public void Array_For()
    {
        for (int iterator = 0; iterator < sampleSetArray.Length; iterator++)
        {
            var item = sampleSetArray[iterator];
            //Perform something
        }
    }

    [Benchmark]
    public void Array_ForEach()
    {
        foreach (int item in sampleSetArray)
        {
            //Perform something
        }
    }

    [Benchmark]
    public void Array_ForEachLinq()
    {
        Array.ForEach(sampleSetArray, (item) => { });
    }

    [Benchmark]
    public void Array_ParallelForEach()
    {
        Parallel.ForEach(sampleSetArray, item => { });
    }

    [Benchmark]
    public void Array_ParallelForAll()
    {
        sampleSetArray.AsParallel().ForAll(item => { });
    }

    [Benchmark]
    public void Array_ForEachAsSpan()
    {
        foreach (var item in sampleSetArray.AsSpan())
        {
            //Perform Something
        }
    }

    [Benchmark]
    public void Array_ForMemoryMarshalSpanUnsafe()
    {
        ref var itemRef = ref MemoryMarshal.GetArrayDataReference(sampleSetArray);
        for (int iterator = 0; iterator < sampleSetArray.Length; iterator++)
        {
            var item = Unsafe.Add(ref itemRef, iterator);
            //Perform something
        }
    }

    [Benchmark]
    public void List_For()
    {
        for (int iterator = 0; iterator < sampleSetList.Count(); iterator++)
        {
            var item = sampleSetList[iterator];
            //Perform something
        }
    }

    [Benchmark]
    public void List_Foreach()
    {
        foreach (int item in sampleSetList)
        {
            //Perform something
        }
    }

    [Benchmark]
    public void List_ForEachLinq()
    {
        sampleSetList.ForEach((item) => { });
    }

    [Benchmark]
    public void List_ParallelForEach()
    {
        Parallel.ForEach(sampleSetList, item => { });
    }

    [Benchmark]
    public void List_ParallelForAll()
    {
        sampleSetList.AsParallel().ForAll(item => { });
    }

    [Benchmark]
    public void List_ForEachAsSpanUnsafe()
    {
        foreach (var item in CollectionsMarshal.AsSpan(sampleSetList))
        {
            //Perform Something
        }
    }

    [Benchmark]
    public void Enumerable_For()
    {
        for (int iterator = 0; iterator < sampleSetEnumerable.Count(); iterator++)
        {
            var item = sampleSetEnumerable.ElementAt(iterator);
            //Perform something
        }
    }

    [Benchmark]
    public void Enumerable_Foreach()
    {
        foreach (int item in sampleSetEnumerable)
        {
            //Perform something
        }
    }

    [Benchmark]
    public void Enumerable_ForEachLinq()
    {
        sampleSetEnumerable.ForEach((item) => { });
    }

    [Benchmark]
    public void Enumerable_ParallelForEach()
    {
        Parallel.ForEach(sampleSetEnumerable, item => { });
    }

    [Benchmark]
    public void Enumerable_ParallelForAll()
    {
        sampleSetEnumerable.AsParallel().ForAll(item => { });
    }
}
