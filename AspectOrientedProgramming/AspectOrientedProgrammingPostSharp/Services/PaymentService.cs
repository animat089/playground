using AspectOrientedProgrammingPostSharp.Aspects;

namespace AspectOrientedProgrammingPostSharp.Services;

public class PaymentService
{
    [LogAspect]
    public void ProcessPayment(int paymentId)
    {
        // Business logic for payment processing
    }
}
