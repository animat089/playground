using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Models;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using Microsoft.Extensions.Logging;

namespace AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Services;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ILogger<InMemoryOrderRepository> _logger;
    private int _sequence;

    public InMemoryOrderRepository(ILogger<InMemoryOrderRepository> logger)
    {
        _logger = logger;
    }

    public Task<IReadOnlyList<Order>> GetUnprocessedOrdersAsync(int batchSize, CancellationToken cancellationToken)
    {
        var orders = Enumerable
            .Range(1, Math.Max(1, batchSize))
            .Select(i =>
            {
                var id = Interlocked.Increment(ref _sequence);
                return new Order
                {
                    Id = $"ORDER-{id:0000}",
                    Amount = 25m + i,
                    Items = new[] { "Widget-A", "Widget-B" }
                };
            })
            .ToList();

        _logger.LogInformation("Repository returned {Count} unprocessed order(s)", orders.Count);
        return Task.FromResult<IReadOnlyList<Order>>(orders);
    }
}