using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Services;

public sealed class FakePaymentService : IPaymentService
{
    private readonly ILogger<FakePaymentService> _logger;
    private readonly ConcurrentDictionary<string, PaymentTransaction> _charges = new();

    public FakePaymentService(ILogger<FakePaymentService> logger)
    {
        _logger = logger;
    }

    public Task<PaymentTransaction> ChargeAsync(Order order, CancellationToken cancellationToken)
    {
        var tx = new PaymentTransaction
        {
            TransactionId = Guid.NewGuid().ToString("N"),
            OrderId = order.Id,
            Amount = order.Amount
        };

        _charges[tx.TransactionId] = tx;
        _logger.LogInformation("Charged {Amount} for {OrderId} (tx: {TransactionId})", tx.Amount, tx.OrderId, tx.TransactionId);

        return Task.FromResult(tx);
    }

    public Task RefundAsync(PaymentTransaction transaction, CancellationToken cancellationToken)
    {
        if (_charges.TryRemove(transaction.TransactionId, out _))
        {
            _logger.LogWarning("Refunded {Amount} for {OrderId} (tx: {TransactionId})", transaction.Amount, transaction.OrderId, transaction.TransactionId);
        }
        else
        {
            _logger.LogWarning("Refund skipped (already refunded?) tx: {TransactionId}", transaction.TransactionId);
        }

        return Task.CompletedTask;
    }
}