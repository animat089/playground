using AspectOrientedProgrammingPostSharp.Aspects;

namespace AspectOrientedProgrammingPostSharp.Services;

public class ProductService
{
    [CachingAspect]
    public string GetProductDetails(int productId)
    {
        // Simulate a slow operation
        Console.WriteLine("Fetching product details from database...");
        // System.Threading.Thread.Sleep(2000);
        return $"Product {productId} details";
    }
}
