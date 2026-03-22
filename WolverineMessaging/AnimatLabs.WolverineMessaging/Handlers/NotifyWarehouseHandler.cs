using System;
using AnimatLabs.WolverineMessaging.Messages;
using Microsoft.Extensions.Logging;

namespace AnimatLabs.WolverineMessaging.Handlers;

public class NotifyWarehouseHandler
{
    public static WarehouseNotified Handle(
        NotifyWarehouse command,
        ILogger<NotifyWarehouseHandler> log)
    {
        log.LogInformation("Warehouse notified for order {OrderId}: {Quantity}x {Product}",
            command.OrderId, command.Quantity, command.Product);

        return new WarehouseNotified(command.OrderId, DateTime.UtcNow);
    }
}
