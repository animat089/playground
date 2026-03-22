using AnimatLabs.WolverineMessaging.Messages;
using Microsoft.Extensions.Logging;

namespace AnimatLabs.WolverineMessaging.Handlers;

public class OrderCreatedHandler
{
    public static void Handle(
        OrderCreated evt,
        ILogger<OrderCreatedHandler> log)
    {
        log.LogInformation("Event received: Order {OrderId} for {Customer} ({Quantity}x {Product}, {Total:C})",
            evt.OrderId, evt.Customer, evt.Quantity, evt.Product, evt.Total);
    }
}
