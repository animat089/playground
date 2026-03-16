namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderSaga.Services;

public interface IMessageBus
{
    Task PublishAsync<T>(T message, CancellationToken ct) where T : class;
}
