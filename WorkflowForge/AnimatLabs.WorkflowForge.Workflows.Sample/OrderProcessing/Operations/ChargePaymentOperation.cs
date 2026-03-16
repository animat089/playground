using AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Operations;

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
