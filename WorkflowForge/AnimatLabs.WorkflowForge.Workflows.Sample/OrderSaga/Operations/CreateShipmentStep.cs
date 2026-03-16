using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Contracts;
using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Operations;

public sealed class CreateShipmentStep(IMessageBus bus) : WorkflowOperationBase
{
    public override string Name => "CreateShipment";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        var email = foundry.GetPropertyOrDefault<string>(SagaKeys.CustomerEmail) ?? "customer@example.com";
        foundry.Logger.LogInformation("[CreateShipment] Creating shipment for order {OrderId}", orderId);

        await bus.PublishAsync(new CreateShipment(orderId, email), ct).ConfigureAwait(false);
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
