using System.Reactive.Linq;

namespace Animatlabs.ReactiveProgramming.Sample;

internal class RangeSorter
{
    public static void StartSorting()
    {
        var numbers = Observable.Range(1, 10);
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        Console.WriteLine("The sorted range is:");
        evenNumbers.Subscribe(n => Console.WriteLine(n));
        Console.ReadKey();
    }
}