using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Operations;

public sealed class SendConfirmationEmailsOperation : WorkflowOperationBase
{
    private readonly IEmailSender _emailSender;

    public SendConfirmationEmailsOperation(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public override string Name => "SendConfirmationEmails";

    public override bool SupportsRestore => true;

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        var orders = foundry.GetPropertyOrDefault<IReadOnlyList<Order>>(ReconciliationKeys.Orders)
            ?? Array.Empty<Order>();

        foundry.Logger.LogInformation("Sending confirmations for {Count} order(s)", orders.Count);

        foreach (var order in orders)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _emailSender.SendConfirmationAsync(order.Id, cancellationToken).ConfigureAwait(false);
        }

        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
        => Task.CompletedTask;
}