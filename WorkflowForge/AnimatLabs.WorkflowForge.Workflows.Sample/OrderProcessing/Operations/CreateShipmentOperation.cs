using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Operations;

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
