using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Contracts;
using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Services;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;

namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Operations;

public sealed class ChargePaymentStep(IMessageBus bus, bool simulateFailure) : WorkflowOperationBase
{
    public override string Name => "ChargePayment";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        var amount = foundry.GetPropertyOrDefault<decimal>(SagaKeys.Amount);
        foundry.Logger.LogInformation("[ChargePayment] Charging ${Amount} for order {OrderId}", amount, orderId);

        await bus.PublishAsync(new ChargePayment(orderId, amount), ct).ConfigureAwait(false);
        await Task.Delay(800, ct).ConfigureAwait(false);

        if (simulateFailure)
        {
            foundry.Logger.LogError("[ChargePayment] Payment gateway timeout for order {OrderId}", orderId);
            throw new InvalidOperationException($"Payment gateway timeout for order {orderId}");
        }

        var txId = $"TXN-{Random.Shared.Next(10000, 99999)}";
        foundry.SetProperty(SagaKeys.TransactionId, txId);
        foundry.Logger.LogInformation("[ChargePayment] Payment {TransactionId} charged for order {OrderId}", txId, orderId);

        return inputData;
    }

    public override async Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var orderId = foundry.GetPropertyOrDefault<Guid>(SagaKeys.OrderId);
        var txId = foundry.GetPropertyOrDefault<string>(SagaKeys.TransactionId);

        if (!string.IsNullOrEmpty(txId))
        {
            foundry.Logger.LogWarning("[ChargePayment] COMPENSATING: Refunding {TransactionId} for order {OrderId}", txId, orderId);
            await Task.Delay(500, ct).ConfigureAwait(false);
            foundry.Logger.LogWarning("[ChargePayment] Refund issued for {TransactionId}", txId);
        }
        else
        {
            foundry.Logger.LogInformation("[ChargePayment] No charge to reverse for order {OrderId}", orderId);
        }
    }
}
