using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Operations;

public sealed class ProcessPaymentsOperation : WorkflowOperationBase
{
    private readonly IPaymentService _paymentService;

    public ProcessPaymentsOperation(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public override string Name => "ProcessPayments";

    public override bool SupportsRestore => true;

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        var orders = foundry.GetPropertyOrDefault<IReadOnlyList<Order>>(ReconciliationKeys.Orders)
            ?? Array.Empty<Order>();

        foundry.Logger.LogInformation("Processing payments for {Count} order(s)", orders.Count);

        var transactions = new List<PaymentTransaction>(orders.Count);

        foreach (var order in orders)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var tx = await _paymentService.ChargeAsync(order, cancellationToken).ConfigureAwait(false);
            transactions.Add(tx);
        }

        foundry.SetProperty(ReconciliationKeys.PaymentTransactions, transactions);
        foundry.Logger.LogInformation("Processed {Count} payment(s)", transactions.Count);

        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        var transactions = foundry.GetPropertyOrDefault<IReadOnlyList<PaymentTransaction>>(ReconciliationKeys.PaymentTransactions)
            ?? Array.Empty<PaymentTransaction>();

        foundry.Logger.LogWarning("Compensating payments: refunding {Count} transaction(s)", transactions.Count);

        foreach (var tx in transactions)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _paymentService.RefundAsync(tx, cancellationToken).ConfigureAwait(false);
        }
    }
}