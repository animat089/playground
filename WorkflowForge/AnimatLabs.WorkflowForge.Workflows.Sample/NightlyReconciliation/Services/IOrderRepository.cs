using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetUnprocessedOrdersAsync(int batchSize, CancellationToken cancellationToken);
}