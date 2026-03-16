using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Contracts;
using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Operations;

public sealed class ReserveStockStep(IMessageBus bus) : WorkflowOperationBase
{
    public override string Name => "ReserveStock";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        foundry.Logger.LogInformation("[ReserveStock] Reserving stock for order {OrderId}", orderId);

        await bus.PublishAsync(new ReserveStock(orderId, 1), ct).ConfigureAwait(false);
        await Task.Delay(500, ct).ConfigureAwait(false);

        foundry.Logger.LogInformation("[ReserveStock] Stock reserved for order {OrderId}", orderId);
        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        foundry.Logger.LogWarning("[ReserveStock] COMPENSATING: Releasing stock for order {OrderId}", orderId);
        await Task.Delay(300, ct).ConfigureAwait(false);
        foundry.Logger.LogWarning("[ReserveStock] Stock released for order {OrderId}", orderId);
    }
}
