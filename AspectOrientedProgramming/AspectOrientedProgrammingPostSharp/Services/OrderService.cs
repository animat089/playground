using AspectOrientedProgrammingPostSharp.Aspects;

namespace AspectOrientedProgrammingPostSharp.Services;

public class OrderService
{
    [LogAspect]
    public void CreateOrder(int orderId)
    {
        // Business logic for order creation
    }

    [LogAspect]
    [SecurityAspect("Admin")]
    public void CancelOrder(int orderId)
    {
        // Business logic for order cancellation
        Console.WriteLine($"Order {orderId} cancelled successfully.");
    }
}
