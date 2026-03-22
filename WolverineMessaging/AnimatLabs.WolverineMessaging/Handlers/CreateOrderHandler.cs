using System;
using AnimatLabs.WolverineMessaging.Messages;
using Microsoft.Extensions.Logging;

namespace AnimatLabs.WolverineMessaging.Handlers;

public class CreateOrderHandler
{
    public static (OrderCreated, NotifyWarehouse) Handle(
        CreateOrder command,
        ILogger<CreateOrderHandler> log)
    {
        var orderId = Guid.NewGuid();
        log.LogInformation("Order {OrderId} created for {Customer}", orderId, command.Customer);

        var created = new OrderCreated(orderId, command.Customer, command.Product, command.Quantity, command.Total);
        var notify = new NotifyWarehouse(orderId, command.Product, command.Quantity);

        return (created, notify);
    }
}
