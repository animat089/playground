using AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Services;
using MassTransit;

namespace AnimatLabs.MassTransitWorkflowForge.OrderService;

public sealed class MassTransitBus(IBus bus) : IMessageBus
{
    public Task PublishAsync<T>(T message, CancellationToken ct) where T : class
        => bus.Publish(message, ct);
}
