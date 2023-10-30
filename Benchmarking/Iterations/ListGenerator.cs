namespace Iterations;

internal static class ListGenerator
{
    private static readonly Random random = new Random(10_00_000);

    public static IEnumerable<int> GenerateList(int size)
    {
        return Enumerable.Range(1, size).Select(i => random.Next()).AsEnumerable();
    }

    public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
    {
        foreach (T item in @this)
        {
            action(item);
        }
    }
}