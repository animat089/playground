using AnimatLabs.HtmxWorkflowForge.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.HtmxWorkflowForge.Workflows;

public static class OrderKeys
{
    public const string OrderId = "OrderId";
    public const string StockReserved = "StockReserved";
    public const string PaymentCharged = "PaymentCharged";
    public const string ShipmentCreated = "ShipmentCreated";
    public const string ShouldFail = "ShouldFail";
}

public static class OrderProcessingWorkflow
{
    public static IWorkflow Build(IWorkflowEventSink sink, bool shouldFail)
    {
        return WF
            .CreateWorkflow("OrderProcessing")
            .AddOperation(new ValidateOrderOperation(sink))
            .AddOperation(new ReserveStockOperation(sink))
            .AddOperation(new ChargePaymentOperation(sink, shouldFail))
            .AddOperation(new CreateShipmentOperation(sink))
            .AddOperation(new SendNotificationOperation(sink))
            .Build();
    }
}

public sealed class ValidateOrderOperation(IWorkflowEventSink sink) : WorkflowOperationBase
{
    public override string Name => "ValidateOrder";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "running", "Checking order details...");
        await Task.Delay(800, ct).ConfigureAwait(false);

        var orderId = $"ORD-{Random.Shared.Next(1000, 9999)}";
        foundry.SetProperty(OrderKeys.OrderId, orderId);

        sink.Report(Name, "completed", $"Order {orderId} validated");
        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "compensated", "Validation record cleared");
        return Task.CompletedTask;
    }
}

public sealed class ReserveStockOperation(IWorkflowEventSink sink) : WorkflowOperationBase
{
    public override string Name => "ReserveStock";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "running", "Reserving inventory...");
        await Task.Delay(1000, ct).ConfigureAwait(false);

        foundry.SetProperty(OrderKeys.StockReserved, true);

        sink.Report(Name, "completed", "3 items reserved in warehouse");
        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "compensating", "Releasing reserved stock...");
        await Task.Delay(600, ct).ConfigureAwait(false);
        sink.Report(Name, "compensated", "Stock released back to inventory");
    }
}

public sealed class ChargePaymentOperation(IWorkflowEventSink sink, bool shouldFail) : WorkflowOperationBase
{
    public override string Name => "ChargePayment";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "running", "Charging payment method...");
        await Task.Delay(1200, ct).ConfigureAwait(false);

        if (shouldFail)
        {
            sink.Report(Name, "failed", "Payment gateway timeout -- triggering compensation");
            throw new InvalidOperationException("Payment gateway timeout");
        }

        foundry.SetProperty(OrderKeys.PaymentCharged, true);
        sink.Report(Name, "completed", "$149.99 charged to card ending 4242");
        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        if (foundry.GetPropertyOrDefault<bool>(OrderKeys.PaymentCharged))
        {
            sink.Report(Name, "compensating", "Issuing refund...");
            await Task.Delay(800, ct).ConfigureAwait(false);
            sink.Report(Name, "compensated", "$149.99 refunded");
        }
        else
        {
            sink.Report(Name, "compensated", "No charge to reverse");
        }
    }
}

public sealed class CreateShipmentOperation(IWorkflowEventSink sink) : WorkflowOperationBase
{
    public override string Name => "CreateShipment";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "running", "Creating shipping label...");
        await Task.Delay(900, ct).ConfigureAwait(false);

        foundry.SetProperty(OrderKeys.ShipmentCreated, true);

        sink.Report(Name, "completed", "Shipment SHIP-7721 created");
        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "compensating", "Cancelling shipment...");
        await Task.Delay(500, ct).ConfigureAwait(false);
        sink.Report(Name, "compensated", "Shipment cancelled");
    }
}

public sealed class SendNotificationOperation(IWorkflowEventSink sink) : WorkflowOperationBase
{
    public override string Name => "SendNotification";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "running", "Sending confirmation email...");
        await Task.Delay(500, ct).ConfigureAwait(false);

        sink.Report(Name, "completed", "Email sent to customer@example.com");
        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        sink.Report(Name, "compensated", "Cancellation email queued");
        return Task.CompletedTask;
    }
}
