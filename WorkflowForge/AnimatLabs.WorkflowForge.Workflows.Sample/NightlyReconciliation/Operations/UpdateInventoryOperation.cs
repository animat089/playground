using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Operations;

public sealed class UpdateInventoryOperation : WorkflowOperationBase
{
    private readonly IInventoryService _inventoryService;

    public UpdateInventoryOperation(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public override string Name => "UpdateInventory";

    public override bool SupportsRestore => true;

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        var orders = foundry.GetPropertyOrDefault<IReadOnlyList<Order>>(ReconciliationKeys.Orders)
            ?? Array.Empty<Order>();

        foundry.Logger.LogInformation("Updating inventory for {Count} order(s)", orders.Count);

        foreach (var order in orders)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _inventoryService.ReserveAsync(order.Id, order.Items, cancellationToken).ConfigureAwait(false);
        }

        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        var orders = foundry.GetPropertyOrDefault<IReadOnlyList<Order>>(ReconciliationKeys.Orders)
            ?? Array.Empty<Order>();

        foundry.Logger.LogWarning("Compensating inventory: releasing {Count} reservation(s)", orders.Count);

        foreach (var order in orders)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _inventoryService.ReleaseAsync(order.Id, order.Items, cancellationToken).ConfigureAwait(false);
        }
    }
}