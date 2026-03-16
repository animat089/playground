using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Operations;

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
