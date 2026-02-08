namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;

public sealed class PaymentTransaction
{
    public required string TransactionId { get; init; } = null!;

    public required string OrderId { get; init; } = null!;

    public decimal Amount { get; init; }
}