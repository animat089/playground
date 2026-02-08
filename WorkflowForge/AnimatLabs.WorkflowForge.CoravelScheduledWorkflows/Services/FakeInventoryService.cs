using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Services;

public sealed class FakeInventoryService : IInventoryService
{
    private readonly ILogger<FakeInventoryService> _logger;
    private readonly ConcurrentDictionary<string, IReadOnlyList<string>> _reservations = new();

    public FakeInventoryService(ILogger<FakeInventoryService> logger)
    {
        _logger = logger;
    }

    public Task ReserveAsync(string orderId, IReadOnlyList<string> items, CancellationToken cancellationToken)
    {
        _reservations[orderId] = items;
        _logger.LogInformation("Reserved inventory for {OrderId} ({Count} item(s))", orderId, items.Count);
        return Task.CompletedTask;
    }

    public Task ReleaseAsync(string orderId, IReadOnlyList<string> items, CancellationToken cancellationToken)
    {
        _reservations.TryRemove(orderId, out _);
        _logger.LogWarning("Released inventory for {OrderId} ({Count} item(s))", orderId, items.Count);
        return Task.CompletedTask;
    }
}