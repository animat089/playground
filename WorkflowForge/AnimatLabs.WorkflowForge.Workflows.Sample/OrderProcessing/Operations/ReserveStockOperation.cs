using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Operations;

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
