namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;

public interface IInventoryService
{
    Task ReserveAsync(string orderId, IReadOnlyList<string> items, CancellationToken cancellationToken);

    Task ReleaseAsync(string orderId, IReadOnlyList<string> items, CancellationToken cancellationToken);
}