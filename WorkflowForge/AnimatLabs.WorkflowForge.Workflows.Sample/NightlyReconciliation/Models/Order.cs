namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;

public sealed class Order
{
    public required string Id { get; init; } = null!;

    public decimal Amount { get; init; }

    public required IReadOnlyList<string> Items { get; init; } = null!;
}