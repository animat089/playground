using AspectOrientedProgrammingPostSharp.Models;
using AspectOrientedProgrammingPostSharp.Services;

namespace AspectOrientedProgrammingPostSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        LoggingAspect();
        SecurityAspect();
        CachingAspect();
    }

    private static void LoggingAspect()
    {
        PaymentService paymentService = new PaymentService();
        paymentService.ProcessPayment(10);
    }

    private static void SecurityAspect()
    {
        // Security Aspect
        SecurityContext.CurrentUser = new User { Username = "JohnDoe", Role = "User" };

        OrderService orderService = new OrderService();

        try
        {
            orderService.CancelOrder(101);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void CachingAspect()
    {
        // Chaching Aspect
        ProductService productService = new ProductService();

        string result1 = productService.GetProductDetails(101);
        Console.WriteLine(result1);

        string result2 = productService.GetProductDetails(101);
        Console.WriteLine(result2);
    }
}
