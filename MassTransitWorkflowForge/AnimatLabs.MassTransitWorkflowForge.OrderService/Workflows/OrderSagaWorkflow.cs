using AnimatLabs.MassTransitWorkflowForge.Contracts;
using MassTransit;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.MassTransitWorkflowForge.OrderService.Workflows;

public static class SagaKeys
{
    public const string OrderId = "OrderId";
    public const string Amount = "Amount";
    public const string CustomerEmail = "CustomerEmail";
    public const string TransactionId = "TransactionId";
    public const string TrackingNumber = "TrackingNumber";
    public const string ShouldFail = "ShouldFail";
}

public static class OrderSagaWorkflow
{
    public static IWorkflow Build(IBus bus, bool simulatePaymentFailure = false)
    {
        return WF
            .CreateWorkflow("OrderSaga")
            .AddOperation(new ReserveStockStep(bus))
            .AddOperation(new ChargePaymentStep(bus, simulatePaymentFailure))
            .AddOperation(new CreateShipmentStep(bus))
            .Build();
    }
}

public sealed class ReserveStockStep(IBus bus) : WorkflowOperationBase
{
    public override string Name => "ReserveStock";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        foundry.Logger.LogInformation("[ReserveStock] Reserving stock for order {OrderId}", orderId);

        await bus.Publish(new ReserveStock(orderId, 1), ct).ConfigureAwait(false);
        await Task.Delay(500, ct).ConfigureAwait(false);

        foundry.Logger.LogInformation("[ReserveStock] Stock reserved for order {OrderId}", orderId);
        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        foundry.Logger.LogWarning("[ReserveStock] COMPENSATING: Releasing stock for order {OrderId}", orderId);
        await Task.Delay(300, ct).ConfigureAwait(false);
        foundry.Logger.LogWarning("[ReserveStock] Stock released for order {OrderId}", orderId);
    }
}

public sealed class ChargePaymentStep(IBus bus, bool simulateFailure) : WorkflowOperationBase
{
    public override string Name => "ChargePayment";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        var amount = foundry.GetPropertyOrDefault<decimal>(SagaKeys.Amount);
        foundry.Logger.LogInformation("[ChargePayment] Charging ${Amount} for order {OrderId}", amount, orderId);

        await bus.Publish(new ChargePayment(orderId, amount), ct).ConfigureAwait(false);
        await Task.Delay(800, ct).ConfigureAwait(false);

        if (simulateFailure)
        {
            foundry.Logger.LogError("[ChargePayment] Payment gateway timeout for order {OrderId}", orderId);
            throw new InvalidOperationException($"Payment gateway timeout for order {orderId}");
        }

        var txId = $"TXN-{Random.Shared.Next(10000, 99999)}";
        foundry.SetProperty(SagaKeys.TransactionId, txId);
        foundry.Logger.LogInformation("[ChargePayment] Payment {TransactionId} charged for order {OrderId}", txId, orderId);

        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        var txId = foundry.GetPropertyOrDefault<string>(SagaKeys.TransactionId);

        if (!string.IsNullOrEmpty(txId))
        {
            foundry.Logger.LogWarning("[ChargePayment] COMPENSATING: Refunding {TransactionId} for order {OrderId}", txId, orderId);
            await Task.Delay(500, ct).ConfigureAwait(false);
            foundry.Logger.LogWarning("[ChargePayment] Refund issued for {TransactionId}", txId);
        }
        else
        {
            foundry.Logger.LogInformation("[ChargePayment] No charge to reverse for order {OrderId}", orderId);
        }
    }
}

public sealed class CreateShipmentStep(IBus bus) : WorkflowOperationBase
{
    public override string Name => "CreateShipment";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        var email = foundry.GetPropertyOrDefault<string>(SagaKeys.CustomerEmail) ?? "customer@example.com";
        foundry.Logger.LogInformation("[CreateShipment] Creating shipment for order {OrderId}", orderId);

        await bus.Publish(new CreateShipment(orderId, email), ct).ConfigureAwait(false);
        await Task.Delay(600, ct).ConfigureAwait(false);

        var tracking = $"SHIP-{Random.Shared.Next(1000, 9999)}";
        foundry.SetProperty(SagaKeys.TrackingNumber, tracking);
        foundry.Logger.LogInformation("[CreateShipment] Shipment {TrackingNumber} created for order {OrderId}", tracking, orderId);

        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        var tracking = foundry.GetPropertyOrDefault<string>(SagaKeys.TrackingNumber);

        if (!string.IsNullOrEmpty(tracking))
        {
            foundry.Logger.LogWarning("[CreateShipment] COMPENSATING: Cancelling shipment {TrackingNumber} for order {OrderId}", tracking, orderId);
            await Task.Delay(300, ct).ConfigureAwait(false);
            foundry.Logger.LogWarning("[CreateShipment] Shipment {TrackingNumber} cancelled", tracking);
        }
    }
}
