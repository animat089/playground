using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Operations;

public sealed class FetchUnprocessedOrdersOperation : WorkflowOperationBase
{
    private readonly IOrderRepository _orderRepository;

    public FetchUnprocessedOrdersOperation(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public override string Name => "FetchUnprocessedOrders";

    public override bool SupportsRestore => true;

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        var batchSize = foundry.GetPropertyOrDefault<int>(ReconciliationKeys.BatchSize, 3);

        foundry.Logger.LogInformation("Fetching up to {BatchSize} order(s)", batchSize);

        var orders = await _orderRepository
            .GetUnprocessedOrdersAsync(batchSize, cancellationToken)
            .ConfigureAwait(false);

        foundry.SetProperty(ReconciliationKeys.Orders, orders);
        foundry.Logger.LogInformation("Fetched {Count} order(s)", orders.Count);

        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData,
        IWorkflowFoundry foundry,
        CancellationToken cancellationToken)
    {
        foundry.Properties.TryRemove(ReconciliationKeys.Orders, out _);
        return Task.CompletedTask;
    }
}