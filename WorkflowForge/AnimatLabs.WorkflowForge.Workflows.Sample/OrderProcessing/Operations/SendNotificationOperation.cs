using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Operations;

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
