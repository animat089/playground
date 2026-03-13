using System.Threading.Channels;

namespace AnimatLabs.HtmxWorkflowForge.Services;

public sealed class WorkflowEvent
{
    public required string OperationName { get; init; }
    public required string Status { get; init; }
    public required string Detail { get; init; }
}

public sealed class ChannelEventSink : IWorkflowEventSink
{
    private readonly Channel<WorkflowEvent> _channel =
        Channel.CreateUnbounded<WorkflowEvent>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });

    public ChannelReader<WorkflowEvent> Reader => _channel.Reader;

    public void Report(string operationName, string status, string detail)
    {
        _channel.Writer.TryWrite(new WorkflowEvent
        {
            OperationName = operationName,
            Status = status,
            Detail = detail
        });
    }

    public void Complete() => _channel.Writer.TryComplete();
}
